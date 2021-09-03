using System.Collections.Generic;
using System.Linq;
using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Data
{
    /// <summary>
    /// A basic "in memory" database to hold our customer data, has no validation use at your own risk!
    /// Don't want to upsert your values? Use something else!
    /// </summary>
    public class InMemoryCustomerDatabase : IDatabase<Customer>
    {
        private readonly Dictionary<int, Customer> _customerData;

        public InMemoryCustomerDatabase()
        {
            _customerData = new Dictionary<int, Customer>();
        }

        public IEnumerable<Customer> GetData()
        {
            return _customerData.Values.ToList();
        }

        public void InsertData(Customer data)
        {
            _customerData[data.Id] = data;
        }

        public void InsertData(IEnumerable<Customer> data)
        {
            data.ToList().ForEach(InsertData);
        }
    }
}