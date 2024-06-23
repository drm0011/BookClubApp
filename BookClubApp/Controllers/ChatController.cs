using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClubApp.Controllers
{
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet("history/{readingListId}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetChatHistory(int readingListId)
        {
            try
            {
                var chatMessages = await _chatMessageService.GetChatMessages(readingListId);
                if (chatMessages == null)
                {
                    return NotFound();
                }
                return Ok(chatMessages);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
