using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Services
{
    public class ReadingListService : IReadingListService
    {
        private readonly IReadingListRepository _readingListRepository;

        public ReadingListService(IReadingListRepository readingListRepository)
        {
            _readingListRepository = readingListRepository;
        }

        public async Task<bool> AddToReadingList(int userId, string title, string author, int publishYear)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || publishYear <= 0)
            {
                throw new ArgumentException("Invalid book details provided.");
            }

            try
            {
                var readingList = await _readingListRepository.GetReadingListByUserId(userId);
                if (readingList == null)
                {
                    readingList = new ReadingList { UserId = userId };
                    await _readingListRepository.CreateReadingList(readingList);
                }

                bool bookExists = readingList.Items.Any(item => item.Title == title);
                if (bookExists)
                {
                    throw new InvalidOperationException("The book already exists in the reading list.");
                }

                var readingListItem = new ReadingListItem
                {
                    Title = title,
                    Author = author,
                    PublishYear = publishYear,
                    ReadingListId = readingList.Id
                };
                await _readingListRepository.AddReadingListItem(readingListItem);
                return true;
            }
            catch (Exception ex)
            {
                // Log exception (not shown here)
                throw new ApplicationException("An error occurred while adding the book to the reading list.", ex);
            }
        }

        public async Task<ReadingList> GetReadingListMetadataByUserId(int userId)
        {
            return await _readingListRepository.GetReadingListByUserId(userId);
        }

        public async Task<IEnumerable<ReadingListItem>> GetReadingListItems(int userId)
        {
            return await _readingListRepository.GetReadingListItems(userId);
        }

        public async Task<ReadingListItem> GetReadingListItem(int id)
        {
            return await _readingListRepository.GetReadingListItem(id);
        }

        public async Task<bool> UpdateReadingListItem(int id, string title, string author, int publishYear)
        {
            var item = await _readingListRepository.GetReadingListItem(id);
            if (item == null)
            {
                return false;
            }
            item.Title = title;
            item.Author = author;
            item.PublishYear = publishYear;
            await _readingListRepository.UpdateReadingListItem(item);
            return true;
        }

        public async Task<bool> DeleteReadingListItem(int id)
        {
            var readingListItem = new ReadingListItem { Id = id };
            await _readingListRepository.DeleteReadingListItem(readingListItem);
            return true;
        }
    }
}
