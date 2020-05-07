namespace PersonalBudget.Models
{
    public class Release
    {
        public string Id { get; set; }
        public int YearRef { get; set; }
        public int MonthRef { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }

        public Transaction Transaction { get; set; }
    }
}
