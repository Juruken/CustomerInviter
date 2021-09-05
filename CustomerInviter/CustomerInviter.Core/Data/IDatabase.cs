using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CustomerInviter.Core.Data
{
    public interface IDatabase<T>
    {
        Task<IEnumerable<T>> Get(FilterDefinition<T> filter);
        Task<IEnumerable<T>> Get();
        Task Insert(T data);
        Task Insert(IEnumerable<T> data);
        Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update);
    }
}