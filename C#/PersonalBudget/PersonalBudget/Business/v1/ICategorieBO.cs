using PersonalBudget.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalBudget.Business.v1
{
    public interface ICategorieBO
    {
        Task<Categorie> GetById(string id);
        Task<IEnumerable<Categorie>> GetByUserId(string userId);
        Task<int> Save(Categorie categorie);
        Task<int> Update(string id, Categorie categorie);
        Task<int> Delete(string id);
    }
}
