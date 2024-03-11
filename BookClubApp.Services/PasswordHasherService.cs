using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly IPasswordHasher<object> _passwordHasher;

        public PasswordHasherService(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}