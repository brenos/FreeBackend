using ChatClient.Models.v1;
using System.Threading.Tasks;

namespace ChatClient.Chat.v1
{
    public interface IChatClient
    {
        Task ReceiveMessage(Message message);
    }
}
