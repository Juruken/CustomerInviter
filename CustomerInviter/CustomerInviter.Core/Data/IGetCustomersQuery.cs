using System.Collections.Generic;
using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Data
{
    public interface IGetCustomersQuery
    {
        IEnumerable<Customer> Execute();
    }
}