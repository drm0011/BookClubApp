using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
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
        private readonly IPasswordHasherService _passwordHasherService;
        public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasherService)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<bool> RegisterUser(UserRegistrationModel userModel)
        {
            if (await _userRepository.UserExists(userModel.Username))
            {
                // Handle the case where user already exists
                return false;
            }
            var hashedPassword = _passwordHasherService.HashPassword(userModel.Password);

            var newUser = new Core.Models.User
            {
                Username = userModel.Username,
                Email = userModel.Email,
                PasswordHash = hashedPassword
            };

            await _userRepository.AddUser(newUser);
            return true;
        }

    }

}
