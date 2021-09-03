using System.Collections.Generic;
using System.Linq;
using CustomerInviter.Core.Data;
using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGetCustomersQuery _getCustomersQuery;
        private readonly ICoordinateService _coordinateService;
        public CustomerService(ICoordinateService coordinateService, IGetCustomersQuery getCustomersQuery)
        {
            _coordinateService = coordinateService;
            _getCustomersQuery = getCustomersQuery;
        }
        
        public IEnumerable<Customer> GetCustomersByDistance(Coordinates source, double distance)
        {
            var customers = _getCustomersQuery.Execute()
                .Where(r => _coordinateService.GetDistance(source, r.Location) <= distance)
                .ToList();
            return customers;
        }

        public IEnumerable<Customer> GetCustomersByDistanceInKm(Coordinates source, double distance)
        {
            var customers = _getCustomersQuery.Execute()
                .ToList()
                .Where(r => _coordinateService.GetDistanceInKm(source, r.Location) <= distance);
            return customers;
        }
    }
}