using GameException;
using MongoDB.Driver;
using GameModels.Mongo.v1;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameServices.v1.Database;

namespace GuildData.Business.v1
{
    public class GuildBusiness : IGuildBusiness
    {
        private IMongoDB<Guild> _mongoDB;
        public GuildBusiness(IMongoDB<Guild> mongoDB)
        {
            _mongoDB = mongoDB;
        }

        public async Task<Guild> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ParameterException("ID is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Guild>().Eq(g => g.Id, id);
                Guild guild = await _mongoDB.GetByFilter(filter);
                if (guild == null)
                {
                    throw new NotFoundException("Guild not found");
                }
                return guild;
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

        public async Task<Guild> GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ParameterException("Name is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Guild>().Eq(g => g.Name, name);
                Guild guild = await _mongoDB.GetByFilter(filter);
                if (guild == null)
                {
                    throw new NotFoundException("Guild not found");
                }
                return guild;
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

        public async Task<Guild> Save(Guild guild)
        {
            guild.Id = Guid.NewGuid().ToString();
            guild.CreatedAt = DateTime.UtcNow;
            guild.Players = new List<string> { guild.Owner };
            ValidateGuild(guild);
            try
            {
                await _mongoDB.Save(guild);
                return guild;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task Update(Guild guild)
        {
            ValidateGuild(guild);
            try
            {
                var filter = new FilterDefinitionBuilder<Guild>().Eq(g => g.Id, guild.Id);
                var guildOld = await GetById(guild.Id);
                if (!guildOld.Owner.Equals(guild.Owner))
                {
                    throw new Exception("Do not change owner");
                }
                Guild guildUpdated = await _mongoDB.Update(filter, guild);
                if (guildUpdated == null)
                {
                    throw new NotFoundException("Guild not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public async Task Delete(string id, string ownerId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ParameterException("Id is null");
            }

            var guildOld = await GetById(id);
            if (!guildOld.Owner.Equals(ownerId))
            {
                throw new Exception("Only the owner can excluded");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Guild>().Eq(g => g.Id, id);
                Guild guildDeleted = await _mongoDB.Delete(filter);
                if (guildDeleted == null)
                {
                    throw new NotFoundException("Guild not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AddPlayerInGuild(GuildAddPlayer guildAddPlayer)
        {
            if (string.IsNullOrEmpty(guildAddPlayer.Id))
            {
                throw new ParameterException("Id is null");
            }
            if (string.IsNullOrEmpty(guildAddPlayer.PlayerId))
            {
                throw new ParameterException("Player Id is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Guild>().Eq(g => g.Id, guildAddPlayer.Id);
                var guild = await GetById(guildAddPlayer.Id);
                if (guild.Players.Contains(guildAddPlayer.PlayerId))
                {
                    throw new Exception("Player was added on guild!");
                }
                guild.Players.Add(guildAddPlayer.PlayerId);
                var guildPlayerAdded = await _mongoDB.Update(filter, guild);
                if (guildPlayerAdded == null)
                {
                    throw new NotFoundException("Guild not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ValidateGuild(Guild guild)
        {
            if (guild == null)
            {
                throw new ParameterException("Guild is null");
            }
            if (string.IsNullOrEmpty(guild.Id))
            {
                throw new ParameterException("Guild ID is null or empty");
            }
            if (string.IsNullOrEmpty(guild.Name))
            {
                throw new ParameterException("Guild Name is null or empty");
            }
            if (string.IsNullOrEmpty(guild.Owner))
            {
                throw new ParameterException("Guild Owner is null or empty");
            }
            if (guild.CreatedAt == null)
            {
                throw new ParameterException("Guild CreatedAt is null");
            }
        }
    }
}
