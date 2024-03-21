using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Models
{
    public class Book
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string? Summary { get; set; }
        public string ISBN { get; set; }
        public string? CoverImage { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
