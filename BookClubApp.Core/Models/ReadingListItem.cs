using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Models
{
    public class ReadingListItem
    {
        public int Id { get; init; }
        public int ReadingListId { get; set; }
        public ReadingList ReadingList { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
