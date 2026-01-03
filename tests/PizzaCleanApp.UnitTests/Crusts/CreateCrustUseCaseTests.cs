using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Crusts.Create;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Crusts;

public class CreateCrustUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Crust_fails_validation_when_name_is_empty()
    {
        var validator = new CreateCrustValidator();
        var request = new CreateCrustRequest(
            Name: "",
            Calories: 100,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Crust_fails_validation_when_name_exceeds_max_length()
    {
        var validator = new CreateCrustValidator();
        var longName = new string('A', Crust.MaxLength.Name + 1);
        var request = new CreateCrustRequest(
            Name: longName,
            Calories: 100,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Crust_fails_validation_when_calories_is_negative()
    {
        var validator = new CreateCrustValidator();
        var request = new CreateCrustRequest(
            Name: "Valid Name",
            Calories: -1,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Calories);
    }

    #endregion

    #region Failure outlines

    [Fact]
    public async Task Crust_is_not_created_when_name_already_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateCrustRequest(
            Name: "Thin Crust",
            Calories: 95,
            IsActive: true
        );

        Result<CreateCrustResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.Conflict);
    }

    #endregion

    #region Successful creation outlines

    [Fact]
    public async Task Crust_is_created_when_name_is_unique()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new CreateCrustRequest(
            Name: "Garlic Butter",
            Calories: 130,
            IsActive: true
        );

        Result<CreateCrustResponse> result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        var response = result.Value;
        response.Id.ShouldBeGreaterThan(0);
        response.Name.ShouldBe("Garlic Butter");
        response.Calories.ShouldBe(130);
        response.IsActive.ShouldBeTrue();
    }

    #endregion

    private static CreateCrustUseCase CreateUseCase(IDbContext dbContext)
        => new CreateCrustUseCase(dbContext);
}
