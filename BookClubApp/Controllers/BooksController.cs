using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            if (string.IsNullOrEmpty(q)) return BadRequest("Query is required");

            var result = await _openLibraryService.SearchBooks(q);
            return Ok(result);
        }
    }
}
