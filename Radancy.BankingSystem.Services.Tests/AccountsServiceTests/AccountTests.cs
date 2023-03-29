using Moq.AutoMock;
using Radancy.BankingSystem.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Radancy.BankingSystem.DataAccess;
using Radancy.BankingSystem.DataAccess.Repository;
using Radancy.BankingSystem.Models;
using Bogus;
using Radancy.BankingSystem.Models.Exceptions;
using Radancy.BankingSystem.Models.ViewModels;

namespace Radancy.BankingSystem.Services.Tests.AccountsServiceTests
{
    [TestClass]
    public class AccountTests
    {
        private AccountsService _accountsService;
        private Mock<IBankingDatabaseContext> _mockDbContext;
        private AutoMocker _autoMocker;
        [TestInitialize]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _mockDbContext = _autoMocker.GetMock<IBankingDatabaseContext>();
            _accountsService = _autoMocker.CreateInstance<AccountsService>();
            CreateData();
        }

        public void CreateData()
        {
            var user1 = new Faker<UserProfile>()
                .RuleForType(typeof(string), f => f.Lorem.Word())
                .RuleFor(x => x.ClientId, "client1")
                .Generate();
            var userProfile = new Faker<UserProfile>()
                .RuleForType(typeof(string), f => f.Lorem.Word())
                .RuleFor(x => x.ClientId, "client1")
                .Generate(1).AsQueryable().BuildMockDbSet();
            var acc1 = new Faker<Account>()
                .RuleFor(x => x.AccountNumber, "1001")
                .RuleFor(x => x.User, (faker) => user1)
                .RuleFor(x => x.Balance, 1000)
                .Generate(1).AsQueryable().BuildMockDbSet();
            _mockDbContext.Setup(context => context.Accounts).Returns(acc1.Object);
            _mockDbContext.Setup(context => context.UserProfiles).Returns(userProfile.Object);
        }

        [TestMethod]
        public async Task GetAccountsList_ShouldSucceed()
        {
            var accounts = await _accountsService.GetAccounts();
            accounts.Should().NotBeNull();
            accounts.Count.Should().Be(1);
        }

        [TestMethod]
        public async Task GetSingleAccount_ShouldSucceed()
        {
            var account = await _accountsService.GetAccount("1001");
            account.Should().NotBeNull();
            account.AccountNumber.Should().Be("1001");
        }
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetSingleAccount_ShouldThrowException()
        {
            var account = await _accountsService.GetAccount("1002");
        }
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task DeleteAccount_ShouldThrowException()
        {
             await _accountsService.DeleteAccount("1002");
        }

        [TestMethod]
        public async Task DeleteAccount_ShouldSucceed()
        {
             await _accountsService.DeleteAccount("1001");
            _mockDbContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestException))]
        public async Task CreateAccount_ShouldThrowException_UserProfileDoesNotExist()
        {
            CreateAccountViewModel createAccountViewModel = new CreateAccountViewModel()
            {
                AccountNumber = "1005",
                ClientId = "client12",
                InitialBalance = 120
            };
            var account = await _accountsService.CreateAccount(createAccountViewModel);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRequestException))]
        public async Task CreateAccount_ShouldThrowException_InitialBalanaceLessThan100()
        {
            CreateAccountViewModel createAccountViewModel = new CreateAccountViewModel()
            {
                AccountNumber = "1005",
                ClientId = "client1",
                InitialBalance = 20
            };
            var account = await _accountsService.CreateAccount(createAccountViewModel);
        }

        [TestMethod]
        public async Task CreateAccount_ShouldSucceed()
        {
            CreateAccountViewModel createAccountViewModel = new CreateAccountViewModel()
            {
                AccountNumber = "1005",
                ClientId = "client1",
                InitialBalance = 120
            };
            var account = await _accountsService.CreateAccount(createAccountViewModel);
            account.Should().NotBeNull();
            _mockDbContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
