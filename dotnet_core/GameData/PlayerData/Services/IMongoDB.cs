using MongoDB.Driver;
using PlayerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerData.Services
{
    public interface IMongoDB<T>
    {
        public Task<T> GetByFilter(FilterDefinition<T> filter);
        public void Save(T mongoObject);
        public void Update(T mongoObject);
        public void Delete(string id);
    }
}
