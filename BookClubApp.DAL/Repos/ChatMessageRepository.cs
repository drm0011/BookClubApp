using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClubApp.DAL.Repos
{
    public class ChatMessageRepository : IChatMessageRepository //fix repeating converting code
    {
        private readonly BookClubAppContext _context;

        public ChatMessageRepository(BookClubAppContext context)
        {
            _context = context;
        }

        public async Task AddChatMessage(ChatMessage chatMessage)
        {
            var entity = ConvertToEntity(chatMessage);
            _context.ChatMessages.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessages(int readingListId)
        {
            var entities = await _context.ChatMessages
                .Where(cm => cm.ReadingListId == readingListId)
                .ToListAsync();

            return entities.Select(e => ConvertToDomainModel(e)).ToList();
        }

        private DAL.Models.ChatMessage ConvertToEntity(Core.Models.ChatMessage chatMessage)
        {
            return new DAL.Models.ChatMessage
            {
                Id = chatMessage.Id,
                Sender = chatMessage.Sender,
                Message = chatMessage.Message,
                Timestamp = chatMessage.Timestamp,
                ReadingListId = chatMessage.ReadingListId,
                ReadingList = ConvertToEntity(chatMessage.ReadingList)
            };
        }

        private Core.Models.ChatMessage ConvertToDomainModel(DAL.Models.ChatMessage entity)
        {
            return new Core.Models.ChatMessage
            {
                Id = entity.Id,
                Sender = entity.Sender,
                Message = entity.Message,
                Timestamp = entity.Timestamp,
                ReadingListId = entity.ReadingListId,
                ReadingList = ConvertToDomainModel(entity.ReadingList)
            };
        }

        private DAL.Models.ReadingList ConvertToEntity(Core.Models.ReadingList readingList)
        {
            if (readingList == null) return null;

            return new DAL.Models.ReadingList
            {
                Id = readingList.Id,
                UserId = readingList.UserId,
                Items = readingList.Items?.Select(i => new DAL.Models.ReadingListItem
                {
                    Id = i.Id,
                    Title = i.Title,
                    Author = i.Author,
                    PublishYear = i.PublishYear,
                    ReadingListId = i.ReadingListId
                }).ToList(),
                ChatMessages = readingList.ChatMessages?.Select(cm => new DAL.Models.ChatMessage
                {
                    Id = cm.Id,
                    Sender = cm.Sender,
                    Message = cm.Message,
                    Timestamp = cm.Timestamp,
                    ReadingListId = cm.ReadingListId
                }).ToList()
            };
        }

        private Core.Models.ReadingList ConvertToDomainModel(DAL.Models.ReadingList entity)
        {
            if (entity == null) return null;

            return new Core.Models.ReadingList
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Items = entity.Items?.Select(i => new Core.Models.ReadingListItem
                {
                    Id = i.Id,
                    Title = i.Title,
                    Author = i.Author,
                    PublishYear = i.PublishYear,
                    ReadingListId = i.ReadingListId
                }).ToList(),
                ChatMessages = entity.ChatMessages?.Select(cm => new Core.Models.ChatMessage
                {
                    Id = cm.Id,
                    Sender = cm.Sender,
                    Message = cm.Message,
                    Timestamp = cm.Timestamp,
                    ReadingListId = cm.ReadingListId
                }).ToList()
            };
        }
    }
}
