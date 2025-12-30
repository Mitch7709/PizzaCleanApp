using FluentValidation;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Sizes.Create;

public class CreateSizeValidator : AbstractValidator<CreateSizeRequest>
{
    public CreateSizeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Size name is required.")
            .MaximumLength(Size.MaxLength.Name);
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Size price must be greater than 0.");
        RuleFor(x => x.Calories)
            .GreaterThanOrEqualTo(0).WithMessage("Size calories must be greater than or equal to 0.");
    }
}
