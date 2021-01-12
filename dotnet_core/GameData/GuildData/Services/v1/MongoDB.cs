using MongoDB.Driver;
using GuildData.Models.v1;
using System;
using System.Threading.Tasks;

namespace GuildData.Services.v1
{
    public class MongoDB<T> : IMongoDB<T> where T: class
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDB(IGuildDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<T>(settings.GuildsCollectionName);
        }

        public async Task<T> GetByFilter(FilterDefinition<T> filter)
        {
            var documentFinder = await _mongoCollection.FindAsync<T>(filter);
            return await documentFinder.FirstOrDefaultAsync();
        }

        public async Task Save(T mongoObject) =>
            await _mongoCollection.InsertOneAsync(mongoObject);

        public async Task<T> Update(FilterDefinition<T> filter, T mongoObject)
        {
            return await _mongoCollection.FindOneAndReplaceAsync(filter, mongoObject);
        }

        public async Task<T> Delete(FilterDefinition<T> filter)
        {
            return await _mongoCollection.FindOneAndDeleteAsync(filter);
        }
    }
}
