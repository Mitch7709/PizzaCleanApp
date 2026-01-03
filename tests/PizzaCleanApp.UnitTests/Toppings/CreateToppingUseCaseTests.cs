using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Toppings.Create;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Toppings;

public class CreateToppingUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Topping_fails_validation_when_name_is_empty()
    {
        var validator = new CreateToppingValidator();
        var request = new CreateToppingRequest(
            Name: "",
            Price: 1.50m,
            Calories: 50,
            CategoryType: ToppingCategory.Meat,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Topping_fails_validation_when_name_exceeds_max_length()
    {
        var validator = new CreateToppingValidator();
        var longName = new string('A', Topping.MaxLength.Name + 1);
        var request = new CreateToppingRequest(
            Name: longName,
            Price: 1.50m,
            Calories: 50,
            CategoryType: ToppingCategory.Meat,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Topping_fails_validation_when_price_is_not_positive()
    {
        var validator = new CreateToppingValidator();
        var request = new CreateToppingRequest(
            Name: "Valid Name",
            Price: 0m,
            Calories: 50,
            CategoryType: ToppingCategory.Meat,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Price);
    }

    [Fact]
    public async Task Topping_fails_validation_when_calories_is_negative()
    {
        var validator = new CreateToppingValidator();
        var request = new CreateToppingRequest(
            Name: "Valid Name",
            Price: 1.00m,
            Calories: -10,
            CategoryType: ToppingCategory.Vegetable,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Calories);
    }

    #endregion

    #region Failure outlines

    [Fact]
    public async Task Topping_is_not_created_when_name_already_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateToppingRequest(
            Name: "Pepperoni",
            Price: 1.60m,
            Calories: 55,
            CategoryType: ToppingCategory.Meat,
            IsActive: true
        );

        Result<CreateToppingResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.Conflict);
    }

    #endregion

    #region Successful creation outlines

    [Fact]
    public async Task Topping_is_created_when_name_is_unique()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateToppingRequest(
            Name: "Jalapenos",
            Price: 0.90m,
            Calories: 8,
            CategoryType: ToppingCategory.Vegetable,
            IsActive: true
        );

        Result<CreateToppingResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Jalapenos");
        response.Price.ShouldBe(0.90m);
        response.Calories.ShouldBe(8);
        response.CategoryType.ShouldBe(ToppingCategory.Vegetable);
        response.IsActive.ShouldBeTrue();
    }

    #endregion

    private static CreateToppingUseCase CreateUseCase(IDbContext dbContext)
        => new CreateToppingUseCase(dbContext);
}
