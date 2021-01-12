using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameModels.Mongo.v1;

namespace GuildChat.Business.v1
{
    public interface IChatBusiness
    {
        public Task AddToGroup(string playerId, string guildId);
        public Task RemoveFromGroup(string playerId, string guildId);
        public Task SendMessageToGuild(Message message);
        public Task<Player> GetPlayerById(string playerId);
        public Task<Guild> GetGuildById(string guildId);
        public bool IsValidChatPlayer(Player player, Guild guild);
    }
}
