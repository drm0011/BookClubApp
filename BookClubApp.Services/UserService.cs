﻿using BookClubApp.Core.DTOs;
using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUser(UserRegistrationModel userModel)
        {
            if (await _userRepository.UserExists(userModel.Username))
            {
                return false;
            }
            var hashedPassword = _passwordHasherService.HashPassword(userModel.Password);

            var newUser = new Core.Models.User(userModel.Username, userModel.Email);
            newUser.SetPassword(userModel.Password, _passwordHasherService);

            await _userRepository.AddUser(newUser);
            return true;
        }

        public async Task<string> AuthenticateUser(UserLoginModel loginModel)
        {
            var user = await _userRepository.GetUserByUsername(loginModel.Username);
            if (user == null || _passwordHasherService.VerifyPassword(user.PasswordHash, loginModel.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
