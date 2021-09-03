using System.Collections.Generic;
using System.Linq;
using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Data
{
    public class GetCustomersQuery : IGetCustomersQuery
    {
        private readonly IDatabase<Customer> _customerDatabase;

        public GetCustomersQuery(IDatabase<Customer> customerDatabase)
        {
            _customerDatabase = customerDatabase;
        }

        public IEnumerable<Customer> Execute()
        {
            return _customerDatabase.GetData().ToList();
        }
    }
}