using BookClubApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username);
        Task AddUser(User user);
    }
}
