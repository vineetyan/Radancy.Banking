namespace Radancy.BankingSystem.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        public UserProfile User { get; set; }
    }
}