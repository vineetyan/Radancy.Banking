using Microsoft.EntityFrameworkCore;
using Radancy.BankingSystem.Models;

namespace Radancy.BankingSystem.DataAccess
{
    public class BankingDatabaseContext : DbContext, IBankingDatabaseContext
    {
        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BankingDb");
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}