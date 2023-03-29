using Microsoft.AspNetCore.Mvc;
using Radancy.BankingSystem.Models;
using Radancy.BankingSystem.Models.ViewModels;
using Radancy.BankingSystem.Services.Accounts;
using System.Net;

namespace Radancy.BankingSystem.Controllers
{
    public class AccountsController : RadancyBankingControllerBase
    {
        
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountsService _accountService;

        public AccountsController(IAccountsService accountService, ILogger<AccountsController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Get list all accounts with user details
        /// </summary>
        /// <returns>List of Accounts</returns>
        [HttpGet]
        [Route("/accounts")]
        [ProducesResponseType(typeof(ApiResponse<Account>), (int)HttpStatusCode.OK)]
        public async Task<ApiResponse<List<Account>>> GetAccounts()
        {
            try { 
            var accounts =  await _accountService.GetAccounts();
                return new ApiResponseBuilder<List<Account>>()
                    .WithData(accounts)
                    .WithHttpStatus(Response, System.Net.HttpStatusCode.OK)
                    .Build();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<Account>>(ex, Response);
            }
        }

        /// <summary>
        /// Get account details of specified account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Account Details</returns>
        [HttpGet]
        [Route("/accounts/{accountNumber}")]
        [ProducesResponseType(typeof(ApiResponse<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<Account>), (int)HttpStatusCode.NotFound)]
        public async Task<ApiResponse<Account>> GetAccount(string accountNumber)
        {
            try
            {
                var account = await _accountService.GetAccount(accountNumber);
                return new ApiResponseBuilder<Account>()
                    .WithData(account)
                    .WithHttpStatus(Response, System.Net.HttpStatusCode.OK)
                    .Build();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Account>(ex, Response);
            }
        }
        [HttpDelete]
        [Route("/accounts/{accountNumber}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ApiResponse> DeleteAccount(string accountNumber)
        {
            try
            {
                await _accountService.DeleteAccount(accountNumber);
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
        [Route("/accounts")]
        [ProducesResponseType(typeof(ApiResponse<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<Account>), (int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<Account>> CreateAccount(CreateAccountViewModel account)
        {
            try
            {
                var accountCreated = await _accountService.CreateAccount(account);
                return new ApiResponseBuilder<Account>()
                        .WithData(accountCreated)
                        .WithHttpStatus(Response, System.Net.HttpStatusCode.OK)
                        .Build();
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Account>(ex, Response);
            }
        }
    }
}