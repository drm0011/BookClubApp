using BookClubApp.Core.Interfaces;
using BookClubApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Core.Models.ReadingList> GetReadingListByUserId(int userId)
        {
            var entity = await _context.ReadingList
                .Include(rl => rl.Items)
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
            _context.ReadingListItem.Add(entity);
            await _context.SaveChangesAsync();
        }

        private Core.Models.ReadingList ConvertToDomainModel(DAL.Models.ReadingList entity)
        {
            return new Core.Models.ReadingList
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Items = entity.Items.Select(i => new Core.Models.ReadingListItem
                {
                    Id = i.Id,
                    ReadingListId = i.ReadingListId,
                    Author=i.Author,
                    Title=i.Title,
                    PublishYear=i.PublishYear
                }).ToList()
            };
        }

        private DAL.Models.ReadingList ConvertToEntity(Core.Models.ReadingList domainModel)
        {
            return new DAL.Models.ReadingList
            {
                Id = domainModel.Id,
                UserId = domainModel.UserId,
                Items = domainModel.Items.Select(i => new DAL.Models.ReadingListItem
                {
                    Id = i.Id,
                    ReadingListId = i.ReadingListId,
                    Author = i.Author,
                    Title = i.Title,
                    PublishYear = i.PublishYear
                }).ToList()
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
