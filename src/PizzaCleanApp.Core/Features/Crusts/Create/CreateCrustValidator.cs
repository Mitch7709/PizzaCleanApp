using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Crusts.Create;

public class CreateCrustValidator : AbstractValidator<CreateCrustRequest>
{
    public CreateCrustValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Crust.MaxLength.Name);
        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0);
    }
}
