using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Crusts.Update;

public class UpdateCrustValidator : AbstractValidator<UpdateCrustRequest>
{
    public UpdateCrustValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Crust.MaxLength.Name);
        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0);
    }
}
