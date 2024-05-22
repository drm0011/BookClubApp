using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookClubApp.Controllers
{
    public class ReadingListController : Controller
    {
        private readonly IReadingListService _readingListService;

        public ReadingListController(IReadingListService readingListService)
        {
            _readingListService = readingListService;
        }

        [HttpPost("readinglist")]
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

        // add DTOs for this api layer instead of using core model?
        [HttpGet("readinglist")]
        public async Task<ActionResult<IEnumerable<ReadingListItem>>> GetReadingListItems(int userId)
        {
            var items = await _readingListService.GetReadingListItems(userId);
            return Ok(items);
        }

        [HttpGet("readinglist/{id}")]
        public async Task<ActionResult<ReadingListItem>> GetReadingListItem(int id)
        {
            var item = await _readingListService.GetReadingListItem(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("readinglist/{id}")]
        public async Task<IActionResult> UpdateReadingListItem(int id, [FromBody] ReadingListItemDto dto)
        {
            var result = await _readingListService.UpdateReadingListItem(id, dto.Title, dto.Author, dto.PublishYear);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Could not update book in the reading list");
            }
        }

        [HttpDelete("readinglist/{id}")]
        public async Task<IActionResult> DeleteReadingListItem(int id)
        {
            var result = await _readingListService.DeleteReadingListItem(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Could not delete book from the reading list");
            }
        }
    }
}
