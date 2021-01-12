using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildChat.Services.v1
{
    public interface IMongoDB<T>
    {
        public Task<T> GetById(FilterDefinition<T> filter);
        public Task<IEnumerable<T>> GetLastTweenty(FilterDefinition<T> filter);
        public Task Save(T mongoObject);
        public Task<T> Update(FilterDefinition<T> filter, T mongoObject);
        public Task<T> Delete(FilterDefinition<T> filter);
    }
}
