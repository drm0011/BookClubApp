using BookClubApp.DAL.Interfaces;
using BookClubApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.DAL.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly BookClubAppContext _context;
        public UserRepository(BookClubAppContext context)
        {
            _context= context;
        }
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
