using System.Collections.Generic;

namespace CustomerInviter.Core.Data
{
    public interface IDatabase<T>
    {
        IEnumerable<T> GetData();
        void InsertData(T data);
        void InsertData(IEnumerable<T> data);
    }
}