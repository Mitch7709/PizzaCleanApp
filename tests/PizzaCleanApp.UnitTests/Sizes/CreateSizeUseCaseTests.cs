using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Sizes.Create;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Sizes;

public class CreateSizeUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Size_fails_validation_when_name_is_empty()
    {
        var validator = new CreateSizeValidator();
        var request = new CreateSizeRequest(
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
        var validator = new CreateSizeValidator();
        var longName = new string('A', Size.MaxLength.Name + 1);
        var request = new CreateSizeRequest(
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
        var validator = new CreateSizeValidator();
        var request = new CreateSizeRequest(
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
        var validator = new CreateSizeValidator();
        var request = new CreateSizeRequest(
            Name: "Valid Name",
            Price: 9.99m,
            Calories: -10
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Calories);
    }

    #endregion

    #region Failure outlines

    [Fact]
    public async Task Size_is_not_created_when_name_already_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateSizeRequest(
            Name: "Small",
            Price: 8.00m,
            Calories: 186
        );

        Result<CreateSizeResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.Conflict);
    }

    #endregion

    #region Successful creation outlines

    [Fact]
    public async Task Size_is_created_when_name_is_unique()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateSizeRequest(
            Name: "Extra Large",
            Price: 14.00m,
            Calories: 320
        );

        Result<CreateSizeResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Extra Large");
        response.Price.ShouldBe(14.00m);
        response.Calories.ShouldBe(320);
    }

    [Fact]
    public async Task Response_contains_persisted_Id_Name_Price_and_Calories_on_success()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateSizeRequest(
            Name: "Family",
            Price: 16.50m,
            Calories: 400
        );

        Result<CreateSizeResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Family");
        response.Price.ShouldBe(16.50m);
        response.Calories.ShouldBe(400);
    }

    #endregion

    private static CreateSizeUseCase CreateUseCase(IDbContext dbContext)
        => new CreateSizeUseCase(dbContext);
}
