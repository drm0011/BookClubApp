using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookClubApp.Core.Models;
using BookClubApp.DAL.Repos;
using BookClubApp.DAL;
using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using BookClubApp.Services;

namespace BookClubApp.Tests
{
    [TestClass]
    public class UserRepositoryIntegrationTests
    {
        private BookClubAppContext _context;
        private UserRepository _repository;
        private IPasswordHasherService _passwordHasherService;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<BookClubAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new BookClubAppContext(options);
            _repository = new UserRepository(_context);
            _passwordHasherService = new MockPasswordHasherService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddUser_ShouldAddUserToDatabase()
        {
            
            var user = new User("testuser", "test@example.com");
            user.SetPassword("hashedpassword", _passwordHasherService);

            
            await _repository.AddUser(user);

            
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            Assert.IsNotNull(result);
            Assert.AreEqual("test@example.com", result.Email);
        }

        [TestMethod]
        public async Task UserExists_ShouldReturnTrueIfUserExists()
        {
            
            var user = new DAL.Models.User
            {
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "hashedpassword"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var exists = await _repository.UserExists("testuser");

            
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task GetUserByUsername_ShouldReturnUser()
        {
            
            var user = new DAL.Models.User
            {
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "hashedpassword"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            
            var result = await _repository.GetUserByUsername("testuser");

            
            Assert.IsNotNull(result);
            Assert.AreEqual("testuser", result.Username);
        }
        public class MockPasswordHasherService : IPasswordHasherService
        {
            public string HashPassword(string password)
            {
                // Simple mock hashing implementation, replace with actual logic if needed
                return password + "hashed";
            }

            public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
            {
                throw new NotImplementedException();
            }
        }
        // Add more tests for other methods in UserRepository...
    }
}
