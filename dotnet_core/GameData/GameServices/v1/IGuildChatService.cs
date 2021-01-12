using GameModels.Mongo.v1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameServices.v1
{
    public interface IGuildChatService
    {
        public Task<List<Message>> GetLastMessages(string guildId);
    }
}
