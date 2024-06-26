using BookClubApp.Core.DTOs;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.Services;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IPasswordHasherService> _passwordHasherServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private Mock<IReadingListRepository> _readingListRepositoryMock;
        private UserService _userService;

        [TestInitialize]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherServiceMock = new Mock<IPasswordHasherService>();
            _configurationMock = new Mock<IConfiguration>();
            _readingListRepositoryMock = new Mock<IReadingListRepository>();

            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("EqsX6G6fc+6ygQgHecgAhV3f0jEarXJG4EHejeChU60=");

            _userService = new UserService(
                _userRepositoryMock.Object,
                _passwordHasherServiceMock.Object,
                _configurationMock.Object,
                _readingListRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task AuthenticateUser_ValidCredentials_ReturnsJwtToken()
        {
            var user = new User("validuser", "valid@example.com");
            user.SetPassword("validpassword", _passwordHasherServiceMock.Object);

            _userRepositoryMock.Setup(repo => repo.GetUserByUsername("validuser")).ReturnsAsync(user);
            _passwordHasherServiceMock.Setup(hasher => hasher.VerifyPassword(user.PasswordHash, "validpassword")).Returns(PasswordVerificationResult.Success);

            var token = await _userService.AuthenticateUser(new UserLoginModel { Username = "validuser", Password = "validpassword" });

            Assert.IsNotNull(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("EqsX6G6fc+6ygQgHecgAhV3f0jEarXJG4EHejeChU60=");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // Debug: print out all claims
            foreach (var claim in jwtToken.Claims)
            {
                Console.WriteLine($"Claim type: {claim.Type}, value: {claim.Value}");
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
            Assert.IsNotNull(userIdClaim, "The JWT token does not contain a 'nameid' claim.");
            Assert.AreEqual(user.Id.ToString(), userIdClaim);
        }

        [TestMethod]
        public async Task AuthenticateUser_InvalidCredentials_ReturnsNull()
        {
            var user = new User("invaliduser", "invalid@example.com");
            user.SetPassword("invalidpassword", _passwordHasherServiceMock.Object);

            _userRepositoryMock.Setup(repo => repo.GetUserByUsername("invaliduser")).ReturnsAsync(user);
            _passwordHasherServiceMock.Setup(hasher => hasher.VerifyPassword(user.PasswordHash, "wrongpassword")).Returns(PasswordVerificationResult.Failed);

            var token = await _userService.AuthenticateUser(new UserLoginModel { Username = "invaliduser", Password = "wrongpassword" });

            Assert.IsNull(token);
        }

        [TestMethod]
        public async Task AuthenticateUser_NonExistentUser_ReturnsNull()
        {
            _userRepositoryMock.Setup(repo => repo.GetUserByUsername("nonexistentuser")).ReturnsAsync((User)null);

            var token = await _userService.AuthenticateUser(new UserLoginModel { Username = "nonexistentuser", Password = "somepassword" });

            Assert.IsNull(token);
        }

        [TestMethod]
        public async Task RegisterUser_WhenUserExists_ReturnsFalse()
        {
            var userModel = new UserRegistrationModel { Username = "existingUser", Email = "user@example.com", Password = "Password123!" };
            _userRepositoryMock.Setup(r => r.UserExists(userModel.Username)).ReturnsAsync(true);

            var result = await _userService.RegisterUser(userModel);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task RegisterUser_WhenUserDoesNotExist_ReturnsTrue()
        {
            var userModel = new UserRegistrationModel { Username = "newUser", Email = "user@example.com", Password = "Password123!" };
            _userRepositoryMock.Setup(r => r.UserExists(userModel.Username)).ReturnsAsync(false);
            _passwordHasherServiceMock.Setup(p => p.HashPassword(userModel.Password)).Returns("HashedPassword");

            var result = await _userService.RegisterUser(userModel);

            _userRepositoryMock.Verify(r => r.AddUser(It.IsAny<User>()), Times.Once);
            Assert.IsTrue(result);
        }
    }
}
