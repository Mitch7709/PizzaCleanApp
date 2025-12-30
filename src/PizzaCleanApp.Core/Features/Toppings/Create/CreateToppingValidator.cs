using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Toppings.Create;

public class CreateToppingValidator : AbstractValidator<CreateToppingRequest>
{
    public CreateToppingValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Topping.MaxLength.Name);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0);
    }
}
