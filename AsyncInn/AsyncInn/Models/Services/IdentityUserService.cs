using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class IdentityUserService : IUser
    {
        private UserManager<ApplicationUser> userManager;
        private JwtTokenService tokenService;

        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService jwtTokenService)
        {
            userManager = manager;
            tokenService = jwtTokenService;
        }

        public async Task<UserDTO> GetUserAsync(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName
            };
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if ( await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDTO
                {
                    Id = user.UserName,
                    Token = await tokenService.GetTokenAsync(user, TimeSpan.FromMinutes(20))
                };
            }
            return null;
        }

        public async Task<UserDTO> Register(RegisterUserDTO data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if(result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, data.Roles);

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetTokenAsync(user, TimeSpan.FromMinutes(20)),
                    Roles = await userManager.GetRolesAsync(user)
                };
            }

            foreach(var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
