using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildChat.Models.v1;

namespace GuildChat.Business.v1
{
    public interface IChatBusiness
    {
        public Task AddToGroup(string playerId, string guildId);
        public Task RemoveFromGroup(string playerId, string guildId);
        public Task SendMessage(Message message);
    }
}
