using GuildChat.Models.v1;
using ChatClient.Chat.v1;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using GuildChat.Business.v1;
using GameException;
using System;

namespace GuildChat.Hubs.v1
{
    public class ChatHub : Hub<IChatClient>
    {
        /**private IMessageBusiness _messageBusiness;

        public ChatHub(IMessageBusiness messageBusiness)
        {
            _messageBusiness = messageBusiness;
        }**/

        public ChatHub() { }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task AddToGroup(string playerId, string groupId)
        {
            await Groups.AddToGroupAsync(playerId, groupId);
        }

        /**public async Task SendMessage(Message message)
        {
            try
            {
                //Validar Player
                //Validar chat
                Message messageSaved = await _messageBusiness.Save(message);
                //await Clients.Group(message.GuildId).ReceiveMessage(message);
                await Clients.All.ReceiveMessage(message);
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
        }**/
    }
}
