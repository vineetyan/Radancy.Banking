using Radancy.BankingSystem.Models;

namespace Radancy.BankingSystem.DataAccess.Repository
{
    public class BankingRepository: IBankingRepository
    {
        private readonly IBankingDatabaseContext _context;
        public BankingRepository(IBankingDatabaseContext context)
        {
            _context = context;
            CreateData();
        }

          public void CreateData()
        {
           
                var userProfile1 = new UserProfile
                {
                    Id = 1,
                    ClientId = "1",
                    Email = "user1@gmail.com",
                    FirstName = "User1Fname",
                    LastName = "User1Lname",
                    PhoneNumber="+919786"
                };
                var userProfile2 = new UserProfile
                {
                    Id = 2,
                    ClientId = "2",
                    Email = "user2@gmail.com",
                    FirstName = "User2Fname",
                    LastName = "User2Lname",
                    PhoneNumber = "+919789"
                };
                var accounts = new List<Account>
                {
                new Account
                {
                    Id=1,
                    Balance=400,
                    AccountNumber="96501",
                    User=userProfile1
                },
                new Account
                {
                    Id=2,
                    Balance=200,
                    AccountNumber="96502",
                    User=userProfile1
                },
                new Account
                {
                    Id=3,
                    Balance=100,
                    AccountNumber="96503",
                    User=userProfile2
                },
                };
            var users = new List<UserProfile> { userProfile1,userProfile2 };
            _context.UserProfiles.AddRange(users);
            _context.Accounts.AddRange(accounts);
            _context.SaveChangesAsync();
            
        }
        
    }
}
