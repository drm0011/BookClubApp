﻿using BookClubApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.DAL
{
    public class BookClubAppContext:DbContext
    {
        public BookClubAppContext(DbContextOptions<BookClubAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}

