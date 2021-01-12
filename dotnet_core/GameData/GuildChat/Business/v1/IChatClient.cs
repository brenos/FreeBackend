using GameModels.Mongo.v1;
using System.Threading.Tasks;

namespace GuildChat.Business.v1
{
    public interface IChatClient
    {
        Task ReceiveMessage(Message message);
    }
}
