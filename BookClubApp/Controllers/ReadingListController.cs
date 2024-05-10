using BookClubApp.Core.Interfaces;
using BookClubApp.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookClubApp.Controllers
{
    public class ReadingListController : Controller
    {
        private readonly IReadingListService _readingListService;
        public ReadingListController(IReadingListService readingListService)
        {
            _readingListService = readingListService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddToReadingList([FromBody] AddToReadingListDto dto)
        {
            var result = await _readingListService.AddToReadingList(dto.UserId, dto.Title, dto.Author, dto.PublishYear);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Could not add book to the reading list");
            }
        }
    }
}
