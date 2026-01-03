using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Toppings.Update;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Toppings;

public class UpdateToppingUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Topping_fails_validation_when_name_is_empty()
    {
        var validator = new UpdateToppingValidator();
        var request = new UpdateToppingRequest(
            Name: "",
            Price: 1.00m,
            Calories: 10,
            CategoryType: ToppingCategory.Vegetable,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Topping_fails_validation_when_name_exceeds_max_length()
    {
        var validator = new UpdateToppingValidator();
        var longName = new string('A', Topping.MaxLength.Name + 1);
        var request = new UpdateToppingRequest(
            Name: longName,
            Price: 1.00m,
            Calories: 10,
            CategoryType: ToppingCategory.Vegetable,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Topping_fails_validation_when_price_is_not_positive()
    {
        var validator = new UpdateToppingValidator();
        var request = new UpdateToppingRequest(
            Name: "Valid Name",
            Price: 0m,
            Calories: 10,
            CategoryType: ToppingCategory.Meat,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Price);
    }

    [Fact]
    public async Task Topping_fails_validation_when_calories_is_negative()
    {
        var validator = new UpdateToppingValidator();
        var request = new UpdateToppingRequest(
            Name: "Valid Name",
            Price: 1.00m,
            Calories: -10,
            CategoryType: ToppingCategory.Meat,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Calories);
    }

    #endregion

    #region Failure outlines

    [Fact]
    public async Task Topping_is_not_updated_when_id_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new UpdateToppingRequest(
            Name: "New Name",
            Price: 1.10m,
            Calories: 12,
            CategoryType: ToppingCategory.Meat,
            IsActive: false
        );

        Result<UpdateToppingResponse> result = await useCase.ExecuteAsync(999, request);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    #endregion

    #region Success outlines

    [Fact]
    public async Task Topping_is_updated_when_request_is_valid()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new UpdateToppingRequest(
            Name: "Updated Pepperoni",
            Price: 1.60m,
            Calories: 60,
            CategoryType: ToppingCategory.Meat,
            IsActive: false
        );

        Result<UpdateToppingResponse> result = await useCase.ExecuteAsync(1, request);
        result.IsSuccess.ShouldBeTrue();
        result.Value.Name.ShouldBe("Updated Pepperoni");
        result.Value.Price.ShouldBe(1.60m);
        result.Value.Calories.ShouldBe(60);
        result.Value.CategoryType.ShouldBe(ToppingCategory.Meat);
        result.Value.IsActive.ShouldBeFalse();
    }

    #endregion

    private static UpdateToppingUseCase CreateUseCase(IDbContext dbContext) => new UpdateToppingUseCase(dbContext);
}
