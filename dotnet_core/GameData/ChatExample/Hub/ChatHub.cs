using System;
using System.Collections.Generic;
using System.Text;
using GameModels.Mongo.v1;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ChatExample.Hub
{
    public class ChatHub : IChatHub
    {
        HubConnection _connection;
        string _uri;
        string _playerId;
        string _guildId;

        public ChatHub(string uri, string playerId, string guildId) 
        {
            _uri = uri;
            _playerId = playerId;
            _guildId = guildId;
        }

        public async Task<string> ConnectAsync(Action<Message> ReceiveMessage, Func<Exception, Task> OnClose)
        {
            try
            {
                _connection = new HubConnectionBuilder()
                    .WithUrl(new Uri(_uri))
                    .WithAutomaticReconnect()
                    .Build();

                _connection.On<Message>("ReceiveMessage", (message) => ReceiveMessage(message));
                _connection.Closed += OnClose;
                _connection.Reconnecting += OnClose;

                await _connection.StartAsync();
                await AddToGroup(_guildId);

                return "Connection started!";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task AddToGroup(string guildId)
        {
            await _connection.InvokeAsync("AddToGroup", guildId);
        }

        public async Task SendMessage(Message message)
        {
            try
            {
                await _connection.InvokeAsync("SendMessage", message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
