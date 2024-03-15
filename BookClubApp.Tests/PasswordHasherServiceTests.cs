using BookClubApp.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Tests
{
    [TestClass]
    public class PasswordHasherServiceTests
    {
        private Mock<IPasswordHasher<object>> _mockPasswordHasher;
        private PasswordHasherService _passwordHasherService;

        [TestInitialize]
        public void Setup()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher<object>>();
            _passwordHasherService = new PasswordHasherService(_mockPasswordHasher.Object);
        }

        [TestMethod]
        public void HashPassword_ReturnsExpectedHash()
        {
            var password = "TestPassword";
            var expectedHash = "HashedPassword";
            _mockPasswordHasher.Setup(ph => ph.HashPassword(null, password)).Returns(expectedHash);

            var result = _passwordHasherService.HashPassword(password);

            Assert.AreEqual(expectedHash, result);
        }

        [TestMethod]
        public void VerifyPassword_CorrectPassword_ReturnsSuccess()
        {
            var hashedPassword = "HashedPassword";
            var password = "TestPassword";
            _mockPasswordHasher.Setup(ph => ph.VerifyHashedPassword(null, hashedPassword, password)).Returns(PasswordVerificationResult.Success);

            var result = _passwordHasherService.VerifyPassword(hashedPassword, password);

            Assert.AreEqual(PasswordVerificationResult.Success, result);
        }
    }
}
