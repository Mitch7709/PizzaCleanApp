using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Pizzas.Update;

public class UpdatePizzaValidator : AbstractValidator<UpdatePizzaRequest>
{
    public UpdatePizzaValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Pizza name is required.")
            .MaximumLength(Pizza.MaxLength.Name);
        RuleFor(x => x.Description)
            .MaximumLength(Pizza.MaxLength.Description);
        RuleFor(x => x.BasePrice)
            .GreaterThan(0).WithMessage("Base price must be greater than zero.");
    }
}
