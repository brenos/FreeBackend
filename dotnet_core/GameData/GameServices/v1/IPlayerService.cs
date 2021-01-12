using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameModels.Mongo.v1;

namespace GameServices.v1
{
    public interface IPlayerService
    {
        public Task<Player> GetById(string playerId);
    }
}
