using System;
using System.Collections.Generic;

namespace PersonalBudget.Models
{
    public class Transaction
    {
        public string Id { get; set; } 
        public string Description { get; set; } 
        public DateTime DtTransaction { get; set; } 
        public int YearRef { get; set; } 
        public int MonthRef { get; set; } 
        public Decimal Amount { get; set; } 
        public string UserId { get; set; }
        public string CategorieId { get; set; }
        public string TypeId { get; set; }

        public Categorie Categorie { get; set; }
        public TransactionType TransactionType { get; set; }

        public ICollection<Release> Releases { get; set; }
    }
}
