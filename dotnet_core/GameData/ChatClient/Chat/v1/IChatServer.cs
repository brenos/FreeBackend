using ChatClient.Models.v1;
using System.Threading.Tasks;

namespace ChatClient.Chat.v1
{
    public interface IChatServer
    {
        Task AddToGroup(string playerId, string guildId);
     
        Task SendMessage(Message message);
    }
}
