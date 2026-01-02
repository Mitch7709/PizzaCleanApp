using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Sizes.Update;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Sizes;

public class UpdateSizeUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Size_fails_validation_when_name_is_empty()
    {
        var validator = new UpdateSizeValidator();
        var request = new UpdateSizeRequest(
            Name: "",
            Price: 9.99m,
            Calories: 200
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Size_fails_validation_when_name_exceeds_max_length()
    {
        var validator = new UpdateSizeValidator();
        var longName = new string('A', Size.MaxLength.Name + 1);
        var request = new UpdateSizeRequest(
            Name: longName,
            Price: 9.99m,
            Calories: 200
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Size_fails_validation_when_price_is_not_positive()
    {
        var validator = new UpdateSizeValidator();
        var request = new UpdateSizeRequest(
            Name: "Valid Name",
            Price: 0m,
            Calories: 200
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Price);
    }

    [Fact]
    public async Task Size_fails_validation_when_calories_is_negative()
    {
        var validator = new UpdateSizeValidator();
        var request = new UpdateSizeRequest(
            Name: "Valid Name",
            Price: 9.99m,
            Calories: -1
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Calories);
    }

    #endregion

    #region Failure outlines

    [Fact]
    public async Task Size_is_not_updated_when_id_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new UpdateSizeRequest(
            Name: "New Name",
            Price: 11.99m,
            Calories: 210
        );

        Result<UpdateSizeResponse> result = await useCase.ExecuteAsync(999, request);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    #endregion

    #region Success outlines

    [Fact]
    public async Task Size_is_updated_when_request_is_valid()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new UpdateSizeRequest(
            Name: "Updated Medium",
            Price: 10.49m,
            Calories: 225
        );

        Result<UpdateSizeResponse> result = await useCase.ExecuteAsync(2, request);
        result.IsSuccess.ShouldBeTrue();
        result.Value.Name.ShouldBe("Updated Medium");
        result.Value.Price.ShouldBe(10.49m);
        // Update use case does not update Calories; seeded Medium calories is 220
        result.Value.Calories.ShouldBe(220);
    }

    #endregion

    private static UpdateSizeUseCase CreateUseCase(IDbContext dbContext) => new UpdateSizeUseCase(dbContext);
}
