using BookClubApp.Controllers;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClubApp.Tests.Controllers
{
    [TestClass]
    public class ChatControllerTests
    {
        private Mock<IChatMessageService> _mockChatMessageService;
        private ChatController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockChatMessageService = new Mock<IChatMessageService>();
            _controller = new ChatController(_mockChatMessageService.Object);
        }

        [TestMethod]
        public async Task GetChatHistory_ShouldReturnOk_WhenMessagesExist()
        {
            
            int readingListId = 1;
            var chatMessages = new List<ChatMessage>
            {
                new ChatMessage { Id = 1, ReadingListId = readingListId, Message = "Test message 1" },
                new ChatMessage { Id = 2, ReadingListId = readingListId, Message = "Test message 2" }
            };
            _mockChatMessageService.Setup(service => service.GetChatMessages(readingListId)).ReturnsAsync(chatMessages);

            
            var result = await _controller.GetChatHistory(readingListId);

            
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(chatMessages, okResult.Value);
        }

        [TestMethod]
        public async Task GetChatHistory_ShouldReturnOk_WhenNoMessagesExist()
        {
            
            int readingListId = 1;
            var chatMessages = new List<ChatMessage>();
            _mockChatMessageService.Setup(service => service.GetChatMessages(readingListId)).ReturnsAsync(chatMessages);

            
            var result = await _controller.GetChatHistory(readingListId);

            
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(chatMessages, okResult.Value);
        }

        [TestMethod]
        public async Task GetChatHistory_ShouldReturnNotFound_WhenServiceReturnsNull()
        {
            
            int readingListId = 1;
            _mockChatMessageService.Setup(service => service.GetChatMessages(readingListId)).ReturnsAsync((IEnumerable<ChatMessage>)null);

            
            var result = await _controller.GetChatHistory(readingListId);

            
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}
