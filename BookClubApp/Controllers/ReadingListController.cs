using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            if (dto == null || string.IsNullOrEmpty(dto.Title) || string.IsNullOrEmpty(dto.Author) || dto.PublishYear <= 0)
            {
                return BadRequest("Invalid book details provided.");
            }

            try
            {
                var result = await _readingListService.AddToReadingList(dto.UserId, dto.Title, dto.Author, dto.PublishYear);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not add book to the reading list.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("readinglist/metadata/{userId}")]
        public async Task<ActionResult<ReadingListMetadataDto>> GetReadingListMetadata(int userId)
        {
            try
            {
                var readingList = await _readingListService.GetReadingListMetadataByUserId(userId);
                if (readingList == null)
                {
                    return NotFound();
                }

                var metadata = new ReadingListMetadataDto
                {
                    ReadingListId = readingList.Id,
                    UserId = readingList.UserId,
                    // Add other metadata if needed
                };

                return Ok(metadata);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("readinglist")]
        public async Task<ActionResult<IEnumerable<ReadingListItem>>> GetReadingListItems(int userId)
        {
            try
            {
                var items = await _readingListService.GetReadingListItems(userId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpGet("readinglist/{id}")]
        public async Task<ActionResult<ReadingListItem>> GetReadingListItem(int id)
        {
            try
            {
                var item = await _readingListService.GetReadingListItem(id);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("readinglist/{id}")]
        public async Task<IActionResult> UpdateReadingListItem(int id, [FromBody] ReadingListItemDto dto)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpDelete("readinglist/{id}")]
        public async Task<IActionResult> DeleteReadingListItem(int id)
        {
            try
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
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
