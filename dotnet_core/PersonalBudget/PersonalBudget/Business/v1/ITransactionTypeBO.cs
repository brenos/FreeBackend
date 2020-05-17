using PersonalBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Business.v1
{
    public interface ITransactionTypeBO
    {
        Task<IEnumerable<TransactionType>> GetAll();
        Task<TransactionType> GetById(string id);
        Task<int> Save(TransactionType transactionType);
        Task<int> Update(string id, TransactionType transactionType);
        Task<int> Delete(string id);
    }
}
