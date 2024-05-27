using BookClubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Interfaces
{
    public interface IChatMessageRepository
    {
        Task AddChatMessage(ChatMessage chatMessage);
        Task<IEnumerable<Core.Models.ChatMessage>> GetChatMessages(int readingListId);
    }
}
