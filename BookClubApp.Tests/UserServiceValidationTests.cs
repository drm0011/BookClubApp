using BookClubApp.Core.DTOs;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace BookClubApp.Tests
{
    [TestClass]
    public class UserServiceValidationTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IPasswordHasherService> _mockPasswordHasherService;
        private Mock<IReadingListRepository> _mockReadingListRepository;
        private Mock<IConfiguration> _mockConfiguration;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasherService = new Mock<IPasswordHasherService>();
            _mockReadingListRepository = new Mock<IReadingListRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _userService = new UserService(_mockUserRepository.Object, _mockPasswordHasherService.Object, _mockConfiguration.Object, _mockReadingListRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Password cannot be empty")]
        public async Task RegisterUser_EmptyPassword_ThrowsException()
        {
            var userModel = new UserRegistrationModel { Username = "testUser", Email = "user@example.com", Password = "" };

            await _userService.RegisterUser(userModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Password must be at least 3 characters long")]
        public async Task RegisterUser_ShortPassword_ThrowsException()
        {
            var userModel = new UserRegistrationModel { Username = "testUser", Email = "user@example.com", Password = "ab" };

            await _userService.RegisterUser(userModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Username cannot be empty")]
        public async Task RegisterUser_EmptyUsername_ThrowsException()
        {
            var userModel = new UserRegistrationModel { Username = "", Email = "user@example.com", Password = "Password123!" };

            await _userService.RegisterUser(userModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "E-mail cannot be empty")]
        public async Task RegisterUser_EmptyEmail_ThrowsException()
        {
            var userModel = new UserRegistrationModel { Username = "testUser", Email = "", Password = "Password123!" };

            await _userService.RegisterUser(userModel);
        }
    }
}
