using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Sizes.Update;

public class UpdateSizeValidator : AbstractValidator<UpdateSizeRequest>
{
    public UpdateSizeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Size.MaxLength.Name);
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0);
    }
}
