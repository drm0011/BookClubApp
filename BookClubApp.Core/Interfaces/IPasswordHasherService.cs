using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClubApp.Core.Interfaces
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);

        PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword);
    }
}