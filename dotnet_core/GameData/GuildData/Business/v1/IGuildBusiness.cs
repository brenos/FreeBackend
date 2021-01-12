using GameModels.Mongo.v1;
using System.Threading.Tasks;

namespace GuildData.Business.v1
{
    public interface IGuildBusiness
    {
        public Task<Guild> GetById(string id);
        public Task<Guild> GetByName(string name);
        public Task<Guild> Save(Guild guild);
        public Task Update(Guild guild);
        public Task Delete(string id, string ownerId);
        public Task AddPlayerInGuild(GuildAddPlayer guildAddPlayer);
    }
}
