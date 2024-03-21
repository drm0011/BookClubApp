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

        private ReadingList _readingList;
        public ReadingList ReadingList
        {
            get => _readingList;
            set => _readingList = value ?? throw new ArgumentNullException(nameof(ReadingList), "ReadingList cannot be null.");
        }

        public User(string username, string email)
        {
            Username = username;
            Email = email;
            _readingList = new ReadingList(); 
        }
        
        public void AddToReadingList(ReadingListItem item)
        {
            if(item==null) throw new ArgumentNullException(nameof(item), "Cannot add null item to reading list.");
            if (_readingList.Items.Contains(item)) throw new InvalidOperationException("This item is already in the reading list.");

            _readingList.Items.Add(item);
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
