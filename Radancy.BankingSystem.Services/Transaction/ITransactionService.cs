using Radancy.BankingSystem.Models;
using Radancy.BankingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radancy.BankingSystem.Services.Transaction
{
    public interface ITransactionService
    {
        public Task WithdrawAmount(TransactAmountViewModel transactAmountViewModel);
        public Task DepositAmount(TransactAmountViewModel transactAmountViewModel);
    }
}
