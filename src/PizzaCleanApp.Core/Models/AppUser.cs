using Microsoft.AspNetCore.Identity;

namespace PizzaCleanApp.Core.Models;

public class AppUser : IdentityUser, IEntity
{
    public AppUser(string email, string firstName, string lastName)
    {
        Email = email;
        UserName = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public static class MaxLengths
    {
        public const int FirstName = 100;
        public const int LastName = 200;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
