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
    }
}
