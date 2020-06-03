using MongoDB.Driver;
using PlayerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerData.Services
{
    public class MongoDB<T> : IMongoDB<T> where T: class
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDB(IPlayerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mongoCollection = database.GetCollection<T>(settings.PlayersCollectionName);
        }

        public async Task<T> GetByFilter(FilterDefinition<T> filter)
        {
            var documentFinder = await _mongoCollection.FindAsync<T>(filter);
            return await documentFinder.FirstOrDefaultAsync();
        }

        public async void Save(T mongoObject) =>
            await _mongoCollection.InsertOneAsync(mongoObject);

        public void Update(T mongoObject)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
