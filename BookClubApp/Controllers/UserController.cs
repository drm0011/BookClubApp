using BookClubApp.DTOs;
using BookClubApp.Core.Interfaces;
using BookClubApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookClubApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = new UserRegistrationModel
            {
                Username = registrationDto.Username,
                Email = registrationDto.Email,
                Password = registrationDto.Password
            };

            var result = await _userService.RegisterUser(userModel); 


            if (result)
            {
                return Ok(new { message = "Registration successful" });
            }
            else
            {
                return BadRequest(new { message = "Registration failed. Username may already be taken." });
            }
        }
    }
}
