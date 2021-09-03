using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerInviter.Core.Models;
using FluentValidation;
using Nancy;
using Newtonsoft.Json;
using Serilog;

namespace CustomerInviter.Api.Service.Services
{
    /// <summary>
    /// Customer file importer is responsible for you guessed it, importing a customer file!
    /// Because our cumstomers love us... they send us their regular import as a .txt file... with JSON.... on separate lines...
    /// Yup you heard me, individual customer json data on separate lines... of a .txt. What a world!
    /// e.g. {"latitude": "51.92893", "user_id": 1, "name": "Alice Cahill", "longitude": "-10.27699"}
    /// </summary>
    public class CustomerFileIngester : ICustomerFileIngester
    {
        private readonly IValidator<CustomerModel> _validator;

        public CustomerFileIngester(IValidator<CustomerModel> validator)
        {
            _validator = validator;
        }

        public async Task<IEnumerable<Customer>> Ingest(Request request)
        {
            if (!request.Files.Any()) return null;

            if (request.Files.Any(f => f.ContentType != "application/text")) return null;

            var customers = new List<Customer>();

            foreach (var file in request.Files.ToList())
            {
                var count = 0;
                using (var stream = new StreamReader(file.Value))
                {
                    while (!stream.EndOfStream)
                    {
                        var line = stream.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;

                        if (!line.Contains("user_id"))
                        {
                            Log.Debug("Skipping line, customer data not detected");
                            continue;;
                        }

                        try
                        {
                            var customerModel = JsonConvert.DeserializeObject<CustomerModel>(line);
                            if (customerModel == null)
                            {
                                Log.Warning("Failed to deserialize CustomerModel");
                                continue;
                            }

                            var validationResult = await _validator.ValidateAsync(customerModel);

                            if (!validationResult.IsValid)
                            {
                                Log.Warning($"Validation errors occurred for a CustomerModel: {string.Join(",", validationResult.Errors.Select(e => e.ErrorMessage))}");
                                continue;
                            }

                            count++;
                            customers.Add(new Customer
                            {
                                Id = customerModel.User_Id,
                                Name = customerModel.Name,
                                Location = new Coordinates(double.Parse(customerModel.Latitude), double.Parse(customerModel.Longitude))
                            });
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "Exception occurred trying to deserialize customer file");
                        }
                    }
                }

                if (count == 0) Log.Warning($"No customers imported from {file.Name}");
            }

            return customers;
        }
    }
}