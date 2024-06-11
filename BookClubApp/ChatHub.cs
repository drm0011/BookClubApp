using Microsoft.AspNetCore.SignalR;
using BookClubApp.Core.Models;
using BookClubApp.Core.Interfaces;
using System.Threading.Tasks;

namespace BookClubApp
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatHub(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        public async Task SendMessage(string sender, string message, int readingListId)
        {
            var chatMessage = new ChatMessage
            {
                Sender = sender,
                Message = message,
                Timestamp = DateTime.UtcNow,
                ReadingListId = readingListId
            };

            await _chatMessageService.AddChatMessage(chatMessage);
            await Clients.All.SendAsync("ReceiveMessage", sender, message, readingListId);
        }
    }
}
