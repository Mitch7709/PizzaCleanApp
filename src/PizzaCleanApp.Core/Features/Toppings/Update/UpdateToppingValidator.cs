using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Toppings.Update;

public class UpdateToppingValidator : AbstractValidator<UpdateToppingRequest>
{
    public UpdateToppingValidator()
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
