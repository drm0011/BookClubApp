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

        [TestMethod]
        public async Task AddReadingListItem_ShouldAddItemToDatabase()
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

            var readingListItem = new ReadingListItem
            {
                Id = 1,
                Title = "Book Title",
                Author = "Author Name",
                PublishYear = 2021,
                ReadingListId = 1
            };

            await _repository.AddReadingListItem(readingListItem);

            var result = await _context.ReadingListItems.FirstOrDefaultAsync(rl => rl.Id == readingListItem.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("Book Title", result.Title);
        }

        [TestMethod]
        public async Task GetReadingListItems_ShouldReturnItems()
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

            var readingListItem = new DAL.Models.ReadingListItem
            {
                Id = 1,
                Title = "Book Title",
                Author = "Author Name",
                PublishYear = 2021,
                ReadingListId = 1
            };
            _context.ReadingListItems.Add(readingListItem);
            await _context.SaveChangesAsync();

            var result = await _repository.GetReadingListItems(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Book Title", result.First().Title);
        }

        [TestMethod]
        public async Task GetReadingListItem_ShouldReturnItem()
        {
            var readingListItem = new DAL.Models.ReadingListItem
            {
                Id = 1,
                Title = "Book Title",
                Author = "Author Name",
                PublishYear = 2021,
                ReadingListId = 1
            };
            _context.ReadingListItems.Add(readingListItem);
            await _context.SaveChangesAsync();

            var result = await _repository.GetReadingListItem(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Book Title", result.Title);
        }

        [TestMethod]
        public async Task DeleteReadingListItem_ShouldRemoveItemFromDatabase()
        {
            var readingListItem = new DAL.Models.ReadingListItem
            {
                Id = 1,
                Title = "Book Title",
                Author = "Author Name",
                PublishYear = 2021,
                ReadingListId = 1
            };
            _context.ReadingListItems.Add(readingListItem);
            await _context.SaveChangesAsync();

            var itemToDelete = new ReadingListItem
            {
                Id = 1,
                Title = "Book Title",
                Author = "Author Name",
                PublishYear = 2021,
                ReadingListId = 1
            };
            await _repository.DeleteReadingListItem(itemToDelete);

            var result = await _context.ReadingListItems.FindAsync(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateReadingListItem_ShouldUpdateItemInDatabase()
        {
            var readingListItem = new DAL.Models.ReadingListItem
            {
                Id = 1,
                Title = "Old Title",
                Author = "Old Author",
                PublishYear = 2020,
                ReadingListId = 1
            };
            _context.ReadingListItems.Add(readingListItem);
            await _context.SaveChangesAsync();

            var updatedItem = new ReadingListItem
            {
                Id = 1,
                Title = "New Title",
                Author = "New Author",
                PublishYear = 2021,
                ReadingListId = 1
            };
            await _repository.UpdateReadingListItem(updatedItem);

            var result = await _context.ReadingListItems.FindAsync(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("New Title", result.Title);
            Assert.AreEqual("New Author", result.Author);
            Assert.AreEqual(2021, result.PublishYear);
        }
    }
}
