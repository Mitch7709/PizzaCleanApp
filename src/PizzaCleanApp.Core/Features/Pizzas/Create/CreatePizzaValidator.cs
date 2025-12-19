using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Pizzas.Create;

public class CreatePizzaValidator : AbstractValidator<CreatePizzaRequest>
{
    public CreatePizzaValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Pizza name is required.")
            .MaximumLength(Pizza.MaxLength.Name);

        RuleFor(x => x.Description)
            .MaximumLength(Pizza.MaxLength.Description);

        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Base price must be greater than zero.");

        When(x => x.ToppingIds is not null, () =>
        {
            RuleForEach(x => x.ToppingIds!)
                .GreaterThan(0).WithMessage("Topping id must be greater than zero.");

            RuleFor(x => x.ToppingIds!)
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("Duplicate toppings are not allowed.");
        });
    }
}
