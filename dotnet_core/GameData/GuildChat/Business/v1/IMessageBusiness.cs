using GameModels.Mongo.v1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildChat.Business.v1
{
    public interface IMessageBusiness
    {
        public Task<Message> GetById(string id);
        public Task<IEnumerable<Message>> GetLastTweenty(string guildId);
        public Task<Message> Save(Message message);
        public Task Delete(string id, string playerId);
    }
}
