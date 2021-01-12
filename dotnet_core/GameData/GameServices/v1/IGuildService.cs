using GameModels.Mongo.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServices.v1
{
    public interface IGuildService
    {
        public Task<Guild> GetById(string guildId);
    }
}
