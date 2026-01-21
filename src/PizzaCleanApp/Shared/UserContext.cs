using PizzaCleanApp.Core.Shared;
using System.Security.Claims;

namespace PizzaCleanApp.API.Shared
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public bool IsAuthenticated()
        {
            return httpContextAccessor
                .HttpContext?
                .User
                .Identity?
                .IsAuthenticated ?? false;
        }
        public string? GetUserId()
        {
            var claimsPrincipal = httpContextAccessor
                .HttpContext?
                .User ??
                throw new ApplicationException("User context is unavailable.");

            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
