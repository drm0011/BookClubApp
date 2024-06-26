using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatMessageService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository ?? throw new ArgumentNullException(nameof(chatMessageRepository));
        }

        public async Task AddChatMessage(ChatMessage chatMessage)
        {
            if (chatMessage == null) throw new ArgumentNullException(nameof(chatMessage));
            await _chatMessageRepository.AddChatMessage(chatMessage);
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages(int readingListId)
        {
            return await _chatMessageRepository.GetChatMessages(readingListId);
        }
    }
}
