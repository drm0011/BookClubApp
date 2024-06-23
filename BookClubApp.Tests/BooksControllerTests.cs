using BookClubApp.Controllers;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookClubApp.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTests
    {
        private Mock<IOpenLibraryService> _mockOpenLibraryService;
        private BooksController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockOpenLibraryService = new Mock<IOpenLibraryService>();
            _controller = new BooksController(_mockOpenLibraryService.Object);
        }

        [TestMethod]
        public async Task Search_ShouldReturnBadRequest_WhenQueryIsEmpty()
        {
            
            var result = await _controller.Search("");

            
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Search_ShouldReturnOk_WhenResultsAreFound()
        {
            
            string query = "Test";
            var searchResults = new List<Book> { new Book { Name = "Test Book", Author = "Test Author" } };
            string serializedResults = JsonConvert.SerializeObject(searchResults);
            _mockOpenLibraryService.Setup(service => service.SearchBooks(query)).ReturnsAsync(serializedResults);

            
            var result = await _controller.Search(query);

            
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(serializedResults, okResult.Value);
        }
    }
}
