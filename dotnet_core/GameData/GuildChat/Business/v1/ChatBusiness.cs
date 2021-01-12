using GuildChat.Hubs.v1;
using GameModels.Mongo.v1;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServices.v1;

namespace GuildChat.Business.v1
{
    public class ChatBusiness : IChatBusiness
    {
        private IHubContext<ChatHub, IChatClient> _context;
        private IMessageBusiness _messageBusiness;
        private IPlayerService _playerService;
        private IGuildService _guildService;

        public ChatBusiness(IHubContext<ChatHub, IChatClient> context, IMessageBusiness messageBusiness, IPlayerService playerService, IGuildService guildService)
        {
            _context = context;
            _messageBusiness = messageBusiness;
            _playerService = playerService;
            _guildService = guildService;
        }

        public async Task AddToGroup(string playerId, string guildId)
        {
            await _context.Groups.AddToGroupAsync(playerId, guildId);
        }

        public async Task RemoveFromGroup(string playerId, string guildId)
        {
            await _context.Groups.RemoveFromGroupAsync(playerId, guildId);
        }

        public async Task SendMessageToGuild(Message message)
        {
            //Validar Player
            Guild guild = await GetGuildById(message.GuildId);
            if (guild == null)
            {
                throw new Exception("Guild not valid!");
            }

            //Preencher dados da mensagem
            message.PlayerId = "1";
            message.PlayerName = "Administrator";
            message.SendAt = DateTime.UtcNow;

            //Validar chat
            Message messageSaved = await _messageBusiness.Save(message);
            await _context.Clients.Group(message.GuildId).ReceiveMessage(message);
        }

        public async Task SendMessageToPlayer(Message message, string playerIdTo)
        {
            //Validar Player
            //Validar chat
            //Message messageSaved = await _messageBusiness.Save(message);
            //await Clients.Group(message.GuildId).ReceiveMessage(message);
            await _context.Clients.Client(playerIdTo).ReceiveMessage(message);
        }

        public async Task<Player> GetPlayerById(string playerId)
        {
            try
            {
                Player player = await _playerService.GetById(playerId);
                return player;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guild> GetGuildById(string guildId)
        {
            try
            {
                Guild guild = await _guildService.GetById(guildId);
                return guild;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsValidChatPlayer(Player player, Guild guild)
        {
            if (player == null)
            {
                return false;
            }
            if (player.GuildId == null)
            {
                return false;
            }
            if (!player.GuildId.Equals(guild.Id))
            {
                return false;
            }
            if (!guild.Players.Exists(g => g.Equals(player.Id)))
            {
                return false;
            }

            return true;
        }
    }
}
