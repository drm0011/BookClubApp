using BookClubApp.Controllers;
using BookClubApp.Core.DTOs;
using BookClubApp.Core.Interfaces;
using BookClubApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsOkResultWithToken()
        {
            var loginDto = new UserLoginDto { Username = "validuser", Password = "validpassword" };
            var token = "sample-jwt-token";
            _userServiceMock.Setup(service => service.AuthenticateUser(It.IsAny<UserLoginModel>())).ReturnsAsync(token);

            var result = await _controller.Login(loginDto);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            //var tokenResult = okResult.Value as dynamic;
            //Assert.AreEqual(token, tokenResult.token);
        }

        [TestMethod]
        public async Task Login_InvalidCredentials_ReturnsUnauthorizedResult()
        {
            var loginDto = new UserLoginDto { Username = "invaliduser", Password = "invalidpassword" };
            _userServiceMock.Setup(service => service.AuthenticateUser(It.IsAny<UserLoginModel>())).ReturnsAsync((string)null);

            var result = await _controller.Login(loginDto);

            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);

            //var messageResult = unauthorizedResult.Value as dynamic;
            //Assert.AreEqual("Invalid username or password", messageResult.message);
        }

        [TestMethod]
        public async Task Login_NonExistentUser_ReturnsUnauthorizedResult()
        {
            var loginDto = new UserLoginDto { Username = "nonexistentuser", Password = "somepassword" };
            _userServiceMock.Setup(service => service.AuthenticateUser(It.IsAny<UserLoginModel>())).ReturnsAsync((string)null);

            var result = await _controller.Login(loginDto);

            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);

            //var messageResult = unauthorizedResult.Value as dynamic;
            //Assert.AreEqual("Invalid username or password", messageResult.message);
        }
    }
}
