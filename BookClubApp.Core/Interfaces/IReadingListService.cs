using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Interfaces
{
    public interface IReadingListService
    {
        Task<bool> AddToReadingList(int userId, string title, string author, int publishYear);
    }
}
