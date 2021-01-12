using ChatClient.Chat.v1;
using GuildChat.Hubs.v1;
using GuildChat.Models.v1;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuildChat.Business.v1
{
    public class ChatBusiness : IChatBusiness
    {
        private IHubContext<ChatHub, IChatClient> _context;
        private IMessageBusiness _messageBusiness;

        public ChatBusiness(IHubContext<ChatHub, IChatClient> context, IMessageBusiness messageBusiness)
        {
            _context = context;
            _messageBusiness = messageBusiness;
        }

        public async Task AddToGroup(string playerId, string guildId)
        {
            await _context.Groups.AddToGroupAsync(playerId, guildId);
        }

        public async Task RemoveFromGroup(string playerId, string guildId)
        {
            await _context.Groups.RemoveFromGroupAsync(playerId, guildId);
        }

        public async Task SendMessage(Message message)
        {
            //Validar Player
            //Validar chat
            //Message messageSaved = await _messageBusiness.Save(message);
            //await Clients.Group(message.GuildId).ReceiveMessage(message);
            await _context.Clients.All.ReceiveMessage(message);
        }
    }
}
