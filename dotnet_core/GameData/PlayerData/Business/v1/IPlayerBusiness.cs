using PlayerData.Models.v1;
using System.Threading.Tasks;

namespace PlayerData.Business.v1
{
    public interface IPlayerBusiness
    {
        public Task<Player> GetById(string id);
        public Task<Player> Save(Player player);
        public Task Update(Player player);
        public Task Delete(string id);
    }
}
