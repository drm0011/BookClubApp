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
        public string Author { get; set; }
        public string Title { get; set; }
        public int? PublishYear { get; set; }
    }
}
