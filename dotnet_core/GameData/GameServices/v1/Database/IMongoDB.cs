using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServices.v1.Database
{
    public interface IMongoDB<T>
    {
        public Task<T> GetByFilter(FilterDefinition<T> filter);
        public Task<IEnumerable<T>> GetEnumerableByFilter(FilterDefinition<T> filter);
        public Task Save(T mongoObject);
        public Task<T> Update(FilterDefinition<T> filter, T mongoObject);
        public Task<T> Delete(FilterDefinition<T> filter);
    }
}
