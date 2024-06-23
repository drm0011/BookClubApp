using BookClubApp.Controllers;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClubApp.Tests.Controllers
{
    [TestClass]
    public class ReadingListControllerTests
    {
        private Mock<IReadingListService> _mockReadingListService;
        private ReadingListController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockReadingListService = new Mock<IReadingListService>();
            _controller = new ReadingListController(_mockReadingListService.Object);
        }

        [TestMethod]
        public async Task AddToReadingList_ShouldReturnBadRequest_WhenDtoIsNull()
        {
            
            var result = await _controller.AddToReadingList(null);

            
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddToReadingList_ShouldReturnBadRequest_WhenDtoHasInvalidData()
        {
            
            var dto = new AddToReadingListDto { Title = "", Author = "", PublishYear = 0 };

            
            var result = await _controller.AddToReadingList(dto);

            
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddToReadingList_ShouldReturnOk_WhenBookIsAddedSuccessfully()
        {
            
            var dto = new AddToReadingListDto { UserId = 1, Title = "Book Title", Author = "Author Name", PublishYear = 2021 };
            _mockReadingListService.Setup(service => service.AddToReadingList(dto.UserId, dto.Title, dto.Author, dto.PublishYear)).ReturnsAsync(true);

            
            var result = await _controller.AddToReadingList(dto);

            
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task GetReadingListMetadata_ShouldReturnNotFound_WhenMetadataIsNull()
        {
            
            int userId = 1;
            _mockReadingListService.Setup(service => service.GetReadingListMetadataByUserId(userId)).ReturnsAsync((ReadingList)null);

            
            var result = await _controller.GetReadingListMetadata(userId);

            
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetReadingListMetadata_ShouldReturnOk_WhenMetadataIsFound()
        {
            
            int userId = 1;
            var readingListMetadata = new ReadingList { Id = 1, UserId = userId };
            _mockReadingListService.Setup(service => service.GetReadingListMetadataByUserId(userId)).ReturnsAsync(readingListMetadata);

            
            var result = await _controller.GetReadingListMetadata(userId);

            
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(okResult.Value, typeof(ReadingListMetadataDto));
        }

        
    }
}
