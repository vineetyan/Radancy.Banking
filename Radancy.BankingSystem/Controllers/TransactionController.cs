using Microsoft.AspNetCore.Mvc;
using Radancy.BankingSystem.Models;
using Radancy.BankingSystem.Models.Exceptions;
using Radancy.BankingSystem.Models.ViewModels;
using Radancy.BankingSystem.Services.Transaction;

namespace Radancy.BankingSystem.Controllers
{
   public class TransactionController : RadancyBankingControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly TransactionService _transactionService;
        public TransactionController(TransactionService transactionService, ILogger<TransactionController> logger)
        {
            _transactionService= transactionService;
            _logger = logger;
        }

        [HttpPost]
        [Route("/withdraw")]
        public async Task<ApiResponse> WithdrawAmount(TransactAmountViewModel transactAmountViewModel)
        {
            if (string.IsNullOrWhiteSpace(transactAmountViewModel.AccountNumber))
            {
                return ExceptionHandler.HandleException(new InvalidRequestException("Account number is missing"), Response);
            }
           
            if (transactAmountViewModel.Amount<=0)
            {
                return ExceptionHandler.HandleException(new InvalidRequestException("Amount should be greater than zero"), Response);
            }
            try
            {
                await _transactionService.WithdrawAmount(transactAmountViewModel);
                return new ApiResponseBuilder()
                            .WithHttpStatus(Response, System.Net.HttpStatusCode.OK)
                            .Build();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex, Response);
            }
        }

        [HttpPost]
        [Route("/deposit")]
        public async Task<ApiResponse> GetAccount(TransactAmountViewModel transactAmountViewModel)
        {
            if (string.IsNullOrWhiteSpace(transactAmountViewModel.AccountNumber))
            {
                return ExceptionHandler.HandleException(new InvalidRequestException("Account number is missing"), Response);
            }

            if (transactAmountViewModel.Amount <= 0)
            {
                return ExceptionHandler.HandleException(new InvalidRequestException("Amount should be greater than zero"), Response);
            }
            try
            {
                await _transactionService.DepositAmount(transactAmountViewModel);
                return new ApiResponseBuilder()
                            .WithHttpStatus(Response, System.Net.HttpStatusCode.OK)
                            .Build();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException(ex, Response);
            }
        }
    }
}