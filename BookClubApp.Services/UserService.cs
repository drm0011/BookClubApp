using BookClubApp.DAL.Interfaces;
using BookClubApp.DAL.Models;
using BookClubApp.Services.Interfaces;
using BookClubApp.Services.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterUser(UserRegistrationModel user)
        {
            if (await _userRepository.UserExists(user.Username))
            {
                // Handle the case where user already exists
                return false;
            }


            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = _passwordHasher.HashPassword(null, user.Password)
            };

            await _userRepository.AddUser(newUser);
            return true;
        }

    }

}
