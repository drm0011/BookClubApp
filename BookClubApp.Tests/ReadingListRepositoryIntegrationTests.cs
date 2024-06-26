using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookClubApp.Core.Models;
using BookClubApp.DAL.Repos;
using BookClubApp.DAL;

namespace BookClubApp.Tests
{
    [TestClass]
    public class ReadingListRepositoryIntegrationTests
    {
        private BookClubAppContext _context;
        private ReadingListRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<BookClubAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new BookClubAppContext(options);
            _repository = new ReadingListRepository(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task CreateReadingList_ShouldAddReadingListToDatabase()
        {
            
            var readingList = new ReadingList
            {
                Id = 1,
                UserId = 1,
                Items = new List<ReadingListItem>(),
                ChatMessages = new List<ChatMessage>()
            };

            
            await _repository.CreateReadingList(readingList);

            
            var result = await _context.ReadingList.FirstOrDefaultAsync(rl => rl.Id == readingList.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        [TestMethod]
        public async Task GetReadingListByUserId_ShouldReturnReadingList()
        {
            
            var readingList = new DAL.Models.ReadingList
            {
                Id = 1,
                UserId = 1,
                Items = new List<DAL.Models.ReadingListItem>(),
                ChatMessages = new List<DAL.Models.ChatMessage>()
            };
            _context.ReadingList.Add(readingList);
            await _context.SaveChangesAsync();

            
            var result = await _repository.GetReadingListByUserId(1);

            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        // Add more tests for other methods in ReadingListRepository...
    }
}
