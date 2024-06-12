using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookClubApp.Core.Models;
using BookClubApp.DAL.Repos;
using BookClubApp.Services;
using BookClubApp.DAL;

namespace BookClubApp.Tests
{
    [TestClass]
    public class ChatMessageServiceIntegrationTests
    {
        private BookClubAppContext _context;
        private ChatMessageRepository _repository;
        private ChatMessageService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<BookClubAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new BookClubAppContext(options);
            _repository = new ChatMessageRepository(_context);
            _service = new ChatMessageService(_repository);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddChatMessage_ShouldAddMessageToDatabase()
        {
            // Arrange
            var chatMessage = new ChatMessage
            {
                Id = 1,
                Sender = "User1",
                Message = "Hello, World!",
                Timestamp = System.DateTime.UtcNow,
                ReadingListId = 1
            };

            // Act
            await _service.AddChatMessage(chatMessage);

            // Assert
            var result = await _context.ChatMessages.FirstOrDefaultAsync(cm => cm.Id == chatMessage.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("Hello, World!", result.Message);
        }

        [TestMethod]
        public async Task GetChatMessages_ShouldReturnMessages()
        {
            // Arrange
            var chatMessage = new ChatMessage
            {
                Id = 1,
                Sender = "User1",
                Message = "Hello, World!",
                Timestamp = System.DateTime.UtcNow,
                ReadingListId = 1
            };
            _context.ChatMessages.Add(new BookClubApp.DAL.Models.ChatMessage
            {
                Id = chatMessage.Id,
                Sender = chatMessage.Sender,
                Message = chatMessage.Message,
                Timestamp = chatMessage.Timestamp,
                ReadingListId = chatMessage.ReadingListId
            });
            await _context.SaveChangesAsync();

            // Act
            var messages = await _service.GetChatMessages(1);

            // Assert
            Assert.AreEqual(1, messages.Count());
            Assert.AreEqual("Hello, World!", messages.First().Message);
        }
    }
}
