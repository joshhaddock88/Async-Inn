using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userService;
        
        public UsersController(IUser service)
        {
            _userService = service;
        }

        [Authorize(Roles = "District Manager")]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            var user = await _userService.Register(data, this.ModelState);
            if (ModelState.IsValid) return user;

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [Authorize]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login (LoginDTO data)
        {
            var userLogin = await _userService.Login(data.Username, data.Password);

            if (userLogin == null) return Unauthorized();

            return userLogin;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> Me()
        {
            return await _userService.GetUserAsync(this.User);
        }
    }
}
