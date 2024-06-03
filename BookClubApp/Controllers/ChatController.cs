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
            var chatMessages = await _chatMessageService.GetChatMessages(readingListId);
            return Ok(chatMessages);
        }
    }
}
