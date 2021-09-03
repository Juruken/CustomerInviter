using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Data
{
    public class CreateCustomerAction : ICreateCustomerAction
    {
        private IDatabase<Customer> _customerDatabase;

        public CreateCustomerAction(IDatabase<Customer> customerDatabase)
        {
            _customerDatabase = customerDatabase;
        }

        public void Execute(Customer newCustomer)
        {
            _customerDatabase.InsertData(newCustomer);
        }
    }
}