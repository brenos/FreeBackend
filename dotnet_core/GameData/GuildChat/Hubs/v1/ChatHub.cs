using GameModels.Mongo.v1;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using GuildChat.Business.v1;
using GameException;
using System;

namespace GuildChat.Hubs.v1
{
    public class ChatHub : Hub<IChatClient>
    {
        private IMessageBusiness _messageBusiness;
        private IChatBusiness _chatBusiness;

        public ChatHub(IMessageBusiness messageBusiness, IChatBusiness chatBusiness)
        {
            _messageBusiness = messageBusiness;
            _chatBusiness = chatBusiness;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = this.Context.User;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddToGroup(string groupId)
        {
            try
            {
                await Groups.AddToGroupAsync(this.Context.ConnectionId, groupId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendMessage(Message message)
        {
            try
            {
                //Validar Player
                Player player = await _chatBusiness.GetPlayerById(message.PlayerId);
                Guild guild = await _chatBusiness.GetGuildById(message.GuildId);
                if (!_chatBusiness.IsValidChatPlayer(player, guild))
                {
                    throw new Exception("Player not valid!");
                }

                //Preencher dados da mensagem
                message.PlayerName = player.Name;
                message.SendAt = DateTime.UtcNow;
                
                //Validar chat
                Message messageSaved = await _messageBusiness.Save(message);
                await Clients.Group(message.GuildId).ReceiveMessage(message);
            }
            catch (NotFoundException ne)
            {
                throw new NotFoundException(ne.Message);
            }
            catch (ParameterException pe)
            {
                throw new ParameterException(pe.Message);
            }
            catch (HubException e)
            {
                throw new HubException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
