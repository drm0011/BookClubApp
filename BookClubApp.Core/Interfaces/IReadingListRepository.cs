using BookClubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Interfaces
{
    public interface IReadingListRepository
    {
        Task<ReadingList> GetReadingListByUserId(int userId);

        Task CreateReadingList(ReadingList readingList);

        Task AddReadingListItem(ReadingListItem readingListItem);

        Task<IEnumerable<ReadingListItem>> GetReadingListItems(int userId);

        Task<ReadingListItem> GetReadingListItem(int id);

        Task UpdateReadingListItem(ReadingListItem item);

        Task DeleteReadingListItem(ReadingListItem item);
    }
}
