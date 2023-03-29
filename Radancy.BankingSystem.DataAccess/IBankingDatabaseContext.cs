using Microsoft.EntityFrameworkCore;
using Radancy.BankingSystem.Models;

namespace Radancy.BankingSystem.DataAccess
{
    public interface IBankingDatabaseContext
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}