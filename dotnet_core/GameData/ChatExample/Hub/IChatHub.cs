using GameModels.Mongo.v1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatExample.Hub
{
    public interface IChatHub
    {
        public Task<string> ConnectAsync(Action<Message> ReceiveMessage, Func<Exception, Task> OnClose);

        public Task SendMessage(Message message);
    }
}
