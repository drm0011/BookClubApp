﻿using BookClubApp.DTOs;
using BookClubApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookClubApp.Core.DTOs;

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
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var loginModel = new UserLoginModel
            {
                Username = loginDto.Username,
                Password = loginDto.Password
            };

            var token = await _userService.AuthenticateUser(loginModel);

            if (token == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new { token });
        }
    }
}
