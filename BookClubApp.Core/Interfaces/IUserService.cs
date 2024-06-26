using BookClubApp.Core.DTOs;
using BookClubApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegistrationModel user);
        Task<string> AuthenticateUser(UserLoginModel loginModel);
        Task<IEnumerable<UserDto>> GetAllUsers();
    }
}
