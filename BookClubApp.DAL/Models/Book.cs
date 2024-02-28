using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.DAL.Models
{
    public class Book // add encapsulation and validation for all models
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required, StringLength(255)]
        public string Author { get; set; }
        [StringLength(2000)]
        public string? Summary { get; set; }
        [Required, StringLength(20)]
        public string ISBN { get; set; }
        [StringLength(2048)]
        public string? CoverImage { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
