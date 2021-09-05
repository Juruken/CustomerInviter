using CustomerInviter.Core.Models;
using MongoDB.Driver;

namespace CustomerInviter.Core.Data
{
    public class CustomerDatabase : MongoDatabase<Customer>
    {
        public CustomerDatabase(IMongoDatabase database) : base(database)
        {
        }
    }
}