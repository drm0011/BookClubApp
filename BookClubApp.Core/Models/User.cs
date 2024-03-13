using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Models
{
    public class User
    {
        public int Id { get; init; }
        private string _username;
        public string Username { 
            get => _username;
            set 
            { 
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentException("Username cannot be empty");
                _username= value;
            }
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("E-mail cannot be empty");
                _email = value;
            }
        }
        public string PasswordHash { get; private set; }
        public void SetPassword(string newPassword, IPasswordHasherService passwordHasherService)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Password cannot be empty");
            }
            if (newPassword.Length < 3) // Example validation
            {
                throw new ArgumentException("Password must be at least 3 characters long");
            }

            // Hash the new password and update the PasswordHash property
            PasswordHash = passwordHasherService.HashPassword(newPassword);
        }

    }
}
