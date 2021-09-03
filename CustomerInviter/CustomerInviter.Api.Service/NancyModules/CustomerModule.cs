using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInviter.Api.Service.Services;
using CustomerInviter.Core.Data;
using CustomerInviter.Core.Models;
using CustomerInviter.Core.Services;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace CustomerInviter.Api.Service.NancyModules
{
    public class CustomerModule : NancyModule
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;
        private readonly IGetCustomersQuery _getCustomersQuery;
        private readonly ICreateCustomerAction _createCustomersAction;
        private readonly ICustomerFileIngester _fileIngester;

        public CustomerModule(IConfiguration configuration, IGetCustomersQuery getCustomersQuery, ICustomerService customerService, ICreateCustomerAction createCustomersAction, ICustomerFileIngester fileIngester) : base("customers")
        {
            _configuration = configuration;
            _getCustomersQuery = getCustomersQuery;
            _customerService = customerService;
            _createCustomersAction = createCustomersAction;
            _fileIngester = fileIngester;

            Put("/import", _=> ImportCustomers());
            Get("/distance/{distance}", _=> GetCustomersByDistance(_.distance));
            Get("/", _=> GetAllCustomers());
            Put("/", _=> AddCustomers());
        }

        public dynamic AddCustomers()
        {
            var model = this.Bind<List<CustomerModel>>();
            var validationResult = this.Validate(model);

            if (!validationResult.IsValid)
                return Negotiate.WithModel(validationResult.FormattedErrors).WithStatusCode(HttpStatusCode.BadRequest);
            
            if (model.Count == 0)
                return Negotiate.WithModel("Must send at least one customer").WithStatusCode(HttpStatusCode.BadRequest);
            
            model.ToList().Select(cm => new Customer
            {
                Id = cm.User_Id,
                Name = cm.Name,
                Location = new Coordinates(double.Parse(cm.Latitude), double.Parse(cm.Longitude))
            })
            .ToList()
            .ForEach(_createCustomersAction.Execute);

            return Negotiate
                .WithStatusCode(HttpStatusCode.Accepted);
        }

        public dynamic GetCustomersByDistance(int distance)
        {
            var headQuarters = new Coordinates(double.Parse(_configuration["Latitude"]), double.Parse(_configuration["Longitude"]));
            var customersByDistance = _customerService.GetCustomersByDistanceInKm(headQuarters, distance).ToList();

            customersByDistance.Sort((a, b) => a.Id > b.Id ? 1 : -1);

            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(customersByDistance.Select(c => new CustomerModel
                {
                    User_Id = c.Id,
                    Latitude = c.Location.Latitude.ToString(),
                    Longitude = c.Location.Longitude.ToString(),
                    Name = c.Name
                }));
        }

        public dynamic GetAllCustomers()
        {
            var customers = _getCustomersQuery.Execute();
            return Negotiate
                .WithStatusCode(HttpStatusCode.OK)
                .WithModel(customers.Select(c => new CustomerModel
                {
                    User_Id = c.Id,
                    Latitude = c.Location.Latitude.ToString(),
                    Longitude = c.Location.Longitude.ToString(),
                    Name = c.Name
                }));
        }

        public async Task<dynamic> ImportCustomers()
        {
            var customers = (await _fileIngester.Ingest(Request))?.ToList();

            if (customers == null || !customers.Any())
                return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

            customers.ForEach(_createCustomersAction.Execute);

            return Negotiate.WithStatusCode(HttpStatusCode.Accepted);
        }
    }
}