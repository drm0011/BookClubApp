using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Models
{
    public class Review
    {
        public int Id { get; init; }
        public string Content { get; set; }
        //[Range(1, 5)]
        public int Rating { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
