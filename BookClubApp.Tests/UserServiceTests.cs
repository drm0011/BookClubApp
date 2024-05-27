using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.Core.DTOs;
using BookClubApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IPasswordHasherService> _mockPasswordHasherService;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasherService = new Mock<IPasswordHasherService>();
            _userService = new UserService(_mockUserRepository.Object, _mockPasswordHasherService.Object);
        }

        [TestMethod]
        public async Task RegisterUser_WhenUserExists_ReturnsFalse()
        {
            var userModel = new UserRegistrationModel { Username = "existingUser", Email = "user@example.com", Password = "Password123!" };
            _mockUserRepository.Setup(r => r.UserExists(userModel.Username)).ReturnsAsync(true);

            var result = await _userService.RegisterUser(userModel);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task RegisterUser_WhenUserDoesNotExist_ReturnsTrue()
        {
            var userModel = new UserRegistrationModel { Username = "newUser", Email = "user@example.com", Password = "Password123!" };
            _mockUserRepository.Setup(r => r.UserExists(userModel.Username)).ReturnsAsync(false);
            _mockPasswordHasherService.Setup(p => p.HashPassword(userModel.Password)).Returns("HashedPassword");

            var result = await _userService.RegisterUser(userModel);

            _mockUserRepository.Verify(r => r.AddUser(It.IsAny<User>()), Times.Once);
            Assert.IsTrue(result);
        }
    }
}
