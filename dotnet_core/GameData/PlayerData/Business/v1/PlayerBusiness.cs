using GameException;
using Microsoft.AspNetCore.Mvc.Versioning;
using MongoDB.Driver;
using PlayerData.Models.v1;
using PlayerData.Services.v1;
using System;
using System.Threading.Tasks;

namespace PlayerData.Business.v1
{
    public class PlayerBusiness : IPlayerBusiness
    {
        private IMongoDB<Player> _mongoDB;
        public PlayerBusiness(IMongoDB<Player> mongoDB)
        {
            _mongoDB = mongoDB;
        }

        public async Task<Player> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ParameterException("ID is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Player>().Eq(p => p.Id, id);
                Player player = await _mongoDB.GetByFilter(filter);
                if (player == null)
                {
                    throw new NotFoundException("Player not found");
                }
                return player;
            }
            catch (NotFoundException ne)
            {
                throw ne;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Player> Save(Player player)
        {
            player.Id = Guid.NewGuid().ToString();
            player.CreatedAt = DateTime.UtcNow;
            ValidatePlayer(player);
            try
            {
                await _mongoDB.Save(player);
                return player;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task Update(Player player)
        {
            ValidatePlayer(player);
            try
            {
                var filter = new FilterDefinitionBuilder<Player>().Eq(p => p.Id, player.Id);
                Player playerUpdated = await _mongoDB.Update(filter, player);
                if (playerUpdated == null)
                {
                    throw new NotFoundException("Player not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public async Task Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ParameterException("Id is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Player>().Eq(p => p.Id, id);
                Player playerDeleted = await _mongoDB.Delete(filter);
                if (playerDeleted == null)
                {
                    throw new NotFoundException("Player not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ValidatePlayer(Player player)
        {
            if (player == null)
            {
                throw new ParameterException("Player is null");
            }
            if (string.IsNullOrEmpty(player.Id))
            {
                throw new ParameterException("Player ID is null or empty");
            }
            if (string.IsNullOrEmpty(player.Name))
            {
                throw new ParameterException("Player Name is null or empty");
            }
            if (player.CreatedAt == null)
            {
                throw new ParameterException("Player CreatedAt is null");
            }
        }
    }
}
