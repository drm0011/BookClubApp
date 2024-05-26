using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Models
{
    public class ReadingList
    {
        public int Id { get; init; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ReadingListItem> Items { get; set; } = new List<ReadingListItem>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>(); 
    }
}
