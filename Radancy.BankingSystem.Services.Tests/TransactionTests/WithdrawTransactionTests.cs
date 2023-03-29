using Moq.AutoMock;
using MockQueryable.Moq;
using Moq;
using Radancy.BankingSystem.DataAccess;
using Radancy.BankingSystem.Models;
using Bogus;
using Radancy.BankingSystem.Models.Exceptions;
using Radancy.BankingSystem.Models.ViewModels;
using Radancy.BankingSystem.Services.Transaction;

namespace Radancy.BankingSystem.Services.Tests.TransactionTests
{
    [TestClass]
    public class WithdrawTransactionTests
    {
        private TransactionService _transactionService;
        private Mock<IBankingDatabaseContext> _mockDbContext;
        private AutoMocker _autoMocker;
        string _accountNumber = "1001";
        string _clientId = "client1";
        decimal _initialBalance = 2000;
        [TestInitialize]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _mockDbContext = _autoMocker.GetMock<IBankingDatabaseContext>();
            _transactionService = _autoMocker.CreateInstance<TransactionService>();
            CreateData();
        }
        public void CreateData()
        {
            var user1 = new Faker<UserProfile>()
                .RuleForType(typeof(string), f => f.Lorem.Word())
                .RuleFor(x => x.ClientId, _clientId)
                .Generate();
            var userProfile = new Faker<UserProfile>()
                .RuleForType(typeof(string), f => f.Lorem.Word())
                .RuleFor(x => x.ClientId, _clientId)
                .Generate(1).AsQueryable().BuildMockDbSet();
            var acc1 = new Faker<Account>()
                .RuleFor(x => x.AccountNumber, _accountNumber)
                .RuleFor(x => x.User, (faker) => user1)
                .RuleFor(x => x.Balance, _initialBalance)
                .Generate(1).AsQueryable().BuildMockDbSet();
            _mockDbContext.Setup(context => context.Accounts).Returns(acc1.Object);
            _mockDbContext.Setup(context => context.UserProfiles).Returns(userProfile.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestException))]
        public async Task WithdrawAmountFromInvalidAccount_ShouldThrowException()
        {
            var deposit = new TransactAmountViewModel
            {
                AccountNumber = "1008",
                Amount = 100
            };
            await _transactionService.WithdrawAmount(deposit);
        }
        [TestMethod]
        [ExpectedException(typeof(WithdrawLimitExceededException))]
        public async Task WithdrawAmountMoreThan90Percent_ShouldThrowException()
        {
            var deposit = new TransactAmountViewModel
            {
                AccountNumber = _accountNumber,
                Amount = 1810
            };
            await _transactionService.WithdrawAmount(deposit);
        }
        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public async Task WithdrawAmountLeavesBalanceLessThanLimit_ShouldThrowException()
        {
            var deposit = new TransactAmountViewModel
            {
                AccountNumber = _accountNumber,
                Amount = 1910
            };
            await _transactionService.WithdrawAmount(deposit);
        }
        [TestMethod]
        public async Task WithdrawAmount_ShouldSucceed()
        {
            var deposit = new TransactAmountViewModel
            {
                AccountNumber = _accountNumber,
                Amount = 100
            };
            await _transactionService.WithdrawAmount(deposit);
            _mockDbContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
