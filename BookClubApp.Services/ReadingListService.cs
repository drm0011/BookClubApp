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
            // ensure user only has a single readinglist
            var readingList = await _readingListRepository.GetReadingListByUserId(userId);
            if (readingList == null)
            {
                readingList = new ReadingList { UserId = userId };
                await _readingListRepository.CreateReadingList(readingList);
            }

            bool bookExists = readingList.Items.Any(item => item.Title == title);
            if (bookExists)
            {
                return false; 
            }

            var readingListItem = new ReadingListItem { Title=title, Author=author, PublishYear=publishYear, ReadingListId = readingList.Id };
            await _readingListRepository.AddReadingListItem(readingListItem);
            return true;
        }
    }
}
