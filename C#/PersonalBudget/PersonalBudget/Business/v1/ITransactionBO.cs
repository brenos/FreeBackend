using PersonalBudget.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalBudget.Business.v1
{
    public interface ITransactionBO
    {
        Task<Transaction> GetById(string id);
        Task<IEnumerable<Transaction>> GetByUserId(string userId, int limit);
        Task<int> Save(Transaction transaction);
        Task<int> Update(string id, Transaction transaction);
        Task<int> Delete(string id);
    }
}
