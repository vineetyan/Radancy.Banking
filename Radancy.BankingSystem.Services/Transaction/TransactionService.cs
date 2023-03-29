using Microsoft.Extensions.Logging;
using Radancy.BankingSystem.DataAccess;
using Radancy.BankingSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Radancy.BankingSystem.Models.Exceptions;
using Radancy.BankingSystem.Common.Constants;

namespace Radancy.BankingSystem.Services.Transaction
{
    public class TransactionService: ITransactionService
    {
        private readonly ILogger<TransactionService> _logger;
        private readonly IBankingDatabaseContext _bankingDatabaseContext;
        public TransactionService(ILogger<TransactionService> logger,
            IBankingDatabaseContext bankingDatabaseContext)
        {
            _logger = logger;
            _bankingDatabaseContext = bankingDatabaseContext;
        }
        public async Task WithdrawAmount(TransactAmountViewModel transactAmountViewModel)
        {
            _logger.LogInformation("Initiating withdraw transaction for {transactAmountViewModel}", transactAmountViewModel);
            var account = await _bankingDatabaseContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == transactAmountViewModel.AccountNumber);
            if (account == null)
            {
                _logger.LogError("Withdraw transaction failed for {transactAmountViewModel}. Account number {accountNumber} does not exist.", transactAmountViewModel, transactAmountViewModel.AccountNumber);
                throw new InvalidRequestException("Given account number does not exist");
            }
            if ((account.Balance - transactAmountViewModel.Amount) < LimitsConstants.MinimumBalance)
            {
                _logger.LogError("Withdraw transaction failed for {transactAmountViewModel}. Insufficient balance", transactAmountViewModel);
                throw new InsufficientFundsException();
            }
            if (transactAmountViewModel.Amount > (account.Balance * LimitsConstants.MaxWithdrawPercentage / 100))
            {
                _logger.LogError("Withdraw transaction failed for {transactAmountViewModel}. Requested amount is greater than 90% of balance", transactAmountViewModel);
                throw new WithdrawLimitExceededException();
            }
            account.Balance = account.Balance - transactAmountViewModel.Amount;
            await _bankingDatabaseContext.SaveChangesAsync();
            _logger.LogInformation("Withdraw transaction successful for {transactAmountViewModel}", transactAmountViewModel);
        }

        public async Task DepositAmount(TransactAmountViewModel transactAmountViewModel)
        {
            _logger.LogInformation("Initiating deposit transaction for {transactAmountViewModel}", transactAmountViewModel);
            var account = await _bankingDatabaseContext.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == transactAmountViewModel.AccountNumber);
            if (account == null)
            {
                _logger.LogError("Deposit transaction failed for {transactAmountViewModel}. Account number {accountNumber} does not exist.", transactAmountViewModel, transactAmountViewModel.AccountNumber);
                throw new InvalidRequestException("Given account number does not exist");
            }
            if (transactAmountViewModel.Amount > LimitsConstants.MaxDepositTransaction)
            {
                _logger.LogError("Deposit transaction failed for {transactAmountViewModel}. Maximum deposit limit exceeded", transactAmountViewModel);
                throw new DepositLimitExceededException();
            }
            account.Balance = account.Balance + transactAmountViewModel.Amount;
            await _bankingDatabaseContext.SaveChangesAsync();
            _logger.LogInformation("Withdraw transaction successful for {transactAmountViewModel}", transactAmountViewModel);
        }
    }
}
