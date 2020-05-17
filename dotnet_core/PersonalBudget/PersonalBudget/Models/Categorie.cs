using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PersonalBudget.Models
{
    public class Categorie
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
