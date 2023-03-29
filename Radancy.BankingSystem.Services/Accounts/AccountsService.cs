using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Radancy.BankingSystem.DataAccess;
using Radancy.BankingSystem.Models;
using Radancy.BankingSystem.Models.Exceptions;
using Radancy.BankingSystem.Models.ViewModels;

namespace Radancy.BankingSystem.Services.Accounts
{
    public class AccountsService : IAccountsService
    {
        private readonly ILogger<AccountsService> _logger;
        private readonly IBankingDatabaseContext _bankingDatabaseContext;
        public AccountsService(ILogger<AccountsService> logger, 
            IBankingDatabaseContext bankingDatabaseContext)
        {
            _logger = logger;
            _bankingDatabaseContext= bankingDatabaseContext;
        }
    
        public async Task<Account> CreateAccount(CreateAccountViewModel   account) {
            _logger.LogInformation("Creating account {account}", account);
            var userProfile = await _bankingDatabaseContext.UserProfiles.FirstOrDefaultAsync(x => x.ClientId == account.ClientId);
            if(userProfile == null)
            {
                _logger.LogError("Could not create account with details as {account}. User with client id {0} does not exist",account, account.ClientId);
                throw new InvalidRequestException("The provided client id does not exist in our system. Please create a user profile first and then create account");
            }
            if (account.InitialBalance < 100)
            {
                _logger.LogError("Could not create account with details as {account}. Initial balance is less than 100.", account);
                throw new InvalidRequestException("Initial balance should be at least $100");
            }
            Account accountToAdd = new Account
            {
                AccountNumber = account.AccountNumber,
                Balance = account.InitialBalance,
                User = userProfile
            };
            _bankingDatabaseContext.Accounts.Add(accountToAdd);
            await _bankingDatabaseContext.SaveChangesAsync();
            _logger.LogInformation("Account created successfully as {account}", account);
            return accountToAdd;
        }
        public async Task DeleteAccount(string accountNumber)
        {
            _logger.LogInformation("Deleting account for account number {accountNumber}", accountNumber);
            var account = await _bankingDatabaseContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                _logger.LogError("Account deletion failed. Account number {accountNumber} does not exist", accountNumber);
                throw new NotFoundException("Given account number does not exist");
            }
            _bankingDatabaseContext.Accounts.Remove(account);
            await _bankingDatabaseContext.SaveChangesAsync();
            _logger.LogInformation("Successfully deleted account {accountNumber}", accountNumber);
        }
        public async Task<Account> GetAccount(string accountNumber)
        {
            _logger.LogInformation("Getting account for account number {accountNumber}", accountNumber);
            var account = await _bankingDatabaseContext.Accounts.Include(a => a.User).FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if(account == null)
            {
                _logger.LogError("Error getting account details. Account number {accountNumber} does not exist", accountNumber);
                throw new NotFoundException("Given account number does not exist");
            }
            _logger.LogInformation("Account details for account number {accountNumber} retrieved successfully", accountNumber);
            return account; 
        }
        public async Task<List<Account>> GetAccounts()
        {
            _logger.LogInformation("Getting list of all accounts");
            var accounts = await _bankingDatabaseContext.Accounts.Include(a => a.User).ToListAsync();
            var count = accounts.Count();
            _logger.LogInformation("Returned {count} number of records", count);
            return accounts;
        }
    }
}
