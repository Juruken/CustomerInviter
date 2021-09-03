using CustomerInviter.Core.Models;

namespace CustomerInviter.Core.Data
{
    public interface ICreateCustomerAction
    {
        void Execute(Customer newCustomer);
    }
}
