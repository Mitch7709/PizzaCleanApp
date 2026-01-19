using PizzaCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Users
{
    public interface ITokenService
    {
        Task<string> GenerateToken(AppUser user);
    }
}
