using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameServices.v1.Database
{
    public class MongoDB<T> : IMongoDB<T> where T: class
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDB(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<T>(settings.CollectionName);
        }

        public async Task<T> GetByFilter(FilterDefinition<T> filter)
        {
            var documentFinder = await _mongoCollection.FindAsync<T>(filter);
            return await documentFinder.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetEnumerableByFilter(FilterDefinition<T> filter)
        {
            var documentFinder = await _mongoCollection.FindAsync<T>(filter);
            return await documentFinder.ToListAsync();
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
