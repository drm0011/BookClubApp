using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using BookClubApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClubApp.DAL.Repos
{
    public class ReadingListRepository : IReadingListRepository
    {
        private readonly BookClubAppContext _context;

        public ReadingListRepository(BookClubAppContext context)
        {
            _context = context;
        }

        //add errorhandling 
        public async Task<Core.Models.ReadingList> GetReadingListByUserId(int userId)
        {
            var entity = await _context.ReadingList
                .Include(rl => rl.Items)
                .Include(rl=>rl.ChatMessages)
                .FirstOrDefaultAsync(rl => rl.UserId == userId);

            return entity == null ? null : ConvertToDomainModel(entity);
        }

        public async Task CreateReadingList(Core.Models.ReadingList readingList)
        {
            var entity = ConvertToEntity(readingList);
            _context.ReadingList.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddReadingListItem(Core.Models.ReadingListItem readingListItem)
        {
            var entity = ConvertToEntity(readingListItem);
            _context.ReadingListItems.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Core.Models.ReadingListItem>> GetReadingListItems(int userId)
        {
            var entities = await _context.ReadingListItems
                .Where(item => item.ReadingList.UserId == userId)
                .ToListAsync();

            return entities.Select(e => ConvertToDomainModel(e)).ToList();
        }

        public async Task<Core.Models.ReadingListItem> GetReadingListItem(int id)
        {
            var entity = await _context.ReadingListItems.FindAsync(id);
            return entity == null ? null : ConvertToDomainModel(entity);
        }

        public async Task DeleteReadingListItem(Core.Models.ReadingListItem readingListItem)
        {
            var existingItem = await _context.ReadingListItems.FindAsync(readingListItem.Id);
            if (existingItem != null)
            {
                _context.ReadingListItems.Remove(existingItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateReadingListItem(Core.Models.ReadingListItem readingListItem)
        {
            var existingItem = await _context.ReadingListItems.FindAsync(readingListItem.Id);
            if (existingItem != null)
            {
                existingItem.Title = readingListItem.Title;
                existingItem.Author = readingListItem.Author;
                existingItem.PublishYear = readingListItem.PublishYear;
                _context.ReadingListItems.Update(existingItem);
                await _context.SaveChangesAsync();
            }
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

        private Core.Models.ReadingListItem ConvertToDomainModel(DAL.Models.ReadingListItem entity)
        {
            return new Core.Models.ReadingListItem
            {
                Id = entity.Id,
                ReadingListId = entity.ReadingListId,
                Author = entity.Author,
                Title = entity.Title,
                PublishYear = entity.PublishYear
            };
        }

        private DAL.Models.ReadingListItem ConvertToEntity(Core.Models.ReadingListItem domainModel)
        {
            return new DAL.Models.ReadingListItem
            {
                Id = domainModel.Id,
                ReadingListId = domainModel.ReadingListId,
                Author = domainModel.Author,
                Title = domainModel.Title,
                PublishYear = domainModel.PublishYear
            };
        }
    }
}
