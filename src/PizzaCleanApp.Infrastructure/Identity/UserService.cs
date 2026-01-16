using Microsoft.AspNetCore.Identity;
using PizzaCleanApp.Core.Features.Users;
using PizzaCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<AppUser?> FindByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public async Task<AppUser?> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            var passwordMatch = await _userManager.CheckPasswordAsync(user, password);

            return passwordMatch ? user : null;
        }

        public async Task<Result<string>> Register(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return user.Id;
            }

            return Result.Failure(ErrorType.DataError, result.Errors.FirstOrDefault()?.Description ?? "Registration failed");
        }
    }
}
