using ChatClient.Models.v1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatExample.Hub
{
    public interface IChatHub
    {
        public Task<string> ConnectAsync(Action<Message> ReceiveMessage);
    }
}
