using System;
using System.Collections.Generic;
using System.Text;
using ChatClient.Chat.v1;
using ChatClient.Models.v1;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ChatExample.Hub
{
    public class ChatHub : IChatHub
    {
        HubConnection connection;
        string _uri;
        string _playerId;
        string _guildId;

        public ChatHub(string uri, string playerId, string guildId) 
        {
            _uri = uri;
            _playerId = playerId;
            _guildId = guildId;
        }

        public async Task<string> ConnectAsync(Action<Message> ReceiveMessage)
        {
            try
            {
                connection = new HubConnectionBuilder()
                    .WithUrl(new Uri(_uri))
                    .WithAutomaticReconnect()
                    .Build();

                connection.On<Message>("ReceiveMessage", (message) => ReceiveMessage(message));

                await connection.StartAsync();
                await AddToGroup(_playerId, _guildId);

                return "Connection started!";

                //messagesList.Items.Add("Connection started");
                //connectButton.IsEnabled = false;
                //sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                return ex.Message;
                //messagesList.Items.Add(ex.Message);
            }
        }

        private async Task AddToGroup(string playerId, string guildId)
        {
            await connection.InvokeAsync("AddToGroup", playerId, guildId);
        }

        //public void ReceiveMessage(Message message)
        //{
        //    Console.WriteLine($"User: {message.PlayerId} - Message: {message.Text}");
        //}

        //public void ReceiveMessage(Message message)
        //{
            //this.Dispatcher.Invoke(() =>
            //{
            //   var newMessage = $"{user}: {message}";
            //    messagesList.Items.Add(newMessage);
            //});
            //Console.WriteLine($"User {playerId} added on guildId {guildId}");
        //}
    }
}
