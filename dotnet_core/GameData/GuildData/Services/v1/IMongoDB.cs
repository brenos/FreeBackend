using MongoDB.Driver;
using System.Threading.Tasks;

namespace GuildData.Services.v1
{
    public interface IMongoDB<T>
    {
        public Task<T> GetByFilter(FilterDefinition<T> filter);
        public Task Save(T mongoObject);
        public Task<T> Update(FilterDefinition<T> filter, T mongoObject);
        public Task<T> Delete(FilterDefinition<T> filter);
    }
}
