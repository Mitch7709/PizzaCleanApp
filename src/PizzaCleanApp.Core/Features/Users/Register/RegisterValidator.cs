using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Users.Register;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(AppUser.MaxLengths.FirstName);
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(AppUser.MaxLengths.LastName);
    }
}
