using PersonalBudget.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Business.v1
{
    public interface IReleaseBO
    {
        Task<Release> GetById(string id);
        Task<IEnumerable<Release>> GetByTransactionId(string transactionId);
        Task<int> Save(Release release);
        Task<int> Update(string id, Release release);
        Task<int> Delete(string id);
    }
}
