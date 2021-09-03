using System.Collections.Generic;
using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomersByDistance(Coordinates source, double distance);
        IEnumerable<Customer> GetCustomersByDistanceInKm(Coordinates source, double distance);
    }
}
