using Radancy.BankingSystem.Models;
using Radancy.BankingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Services.Accounts
{
    public interface IAccountsService
    {
        public Task<Account> CreateAccount(CreateAccountViewModel account);
        public Task DeleteAccount(string accountNumber);
        public Task<Account> GetAccount(string accountNumber);
        public Task<List<Account>> GetAccounts();
    }
}
