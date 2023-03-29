namespace Radancy.BankingSystem.Models
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
