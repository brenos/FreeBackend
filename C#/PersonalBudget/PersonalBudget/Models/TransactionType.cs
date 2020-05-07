using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Models
{
    public class TransactionType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
