using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookClubApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IOpenLibraryService _openLibraryService;

        public BooksController(IOpenLibraryService openLibraryService)
        {
            _openLibraryService = openLibraryService;
        }

        [HttpGet("books")]
        public async Task<IActionResult> Search(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return BadRequest("Query is required");
            }

            try
            {
                var result = await _openLibraryService.SearchBooks(q);
                return Ok(result);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
