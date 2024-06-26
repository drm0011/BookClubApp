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

        public async Task<IEnumerable<Core.Models.User>> GetAllUsers()
        {
            var userEntities = await _context.Users.ToListAsync();
            return userEntities.Select(user => Core.Models.User.Create(user.Id, user.Username, user.Email, user.PasswordHash));
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<Core.Models.User> GetUserByUsername(string username)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (userEntity == null)
            {
                return null;
            }

            return Core.Models.User.Create(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.PasswordHash);
        }
    }
}
