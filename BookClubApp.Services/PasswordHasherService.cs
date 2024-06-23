using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
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
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be null or empty", nameof(password));
            return _passwordHasher.HashPassword(null, password);
        }

        public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword)) throw new ArgumentException("Hashed password cannot be null or empty", nameof(hashedPassword));
            if (string.IsNullOrEmpty(providedPassword)) throw new ArgumentException("Provided password cannot be null or empty", nameof(providedPassword));

            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}