using BookClubApp.Core;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
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
        public async Task AddUser(Core.Models.User userDomain)
        {
            var userEntity = new DAL.Models.User
            {
                Username = userDomain.Username,
                Email = userDomain.Email,
                PasswordHash = userDomain.PasswordHash,
            };
            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
