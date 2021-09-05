using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerInviter.Core.Extensions;
using MongoDB.Driver;

namespace CustomerInviter.Core.Data
{
    public abstract class MongoDatabase<T> : IDatabase<T>
    {
        private IMongoDatabase _database;
        protected MongoDatabase(IMongoDatabase database)
        {
            _database = database;
        }

        public virtual async Task<IEnumerable<T>> Get(FilterDefinition<T> filter)
        {
            var result = await _database.GetCollection<T>().FindAsync(filter);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            var result = await _database.GetCollection<T>().FindAsync(r => true);
            return result.ToList();
        }

        public virtual async Task Insert(T data)
        {
            await _database.GetCollection<T>().InsertOneAsync(data);
        }

        public virtual async Task Insert(IEnumerable<T> data)
        {
            await _database.GetCollection<T>().InsertManyAsync(data);
        }

        public virtual async Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await _database.GetCollection<T>().UpdateOneAsync(filter, update);
        }
    }
}