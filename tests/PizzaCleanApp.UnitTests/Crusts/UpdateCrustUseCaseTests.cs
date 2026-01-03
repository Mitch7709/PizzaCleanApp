using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Crusts.Update;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Crusts;

public class UpdateCrustUseCaseTests
{
    #region Validation outlines

    [Fact]
    public async Task Crust_fails_validation_when_name_is_empty()
    {
        var validator = new UpdateCrustValidator();
        var request = new UpdateCrustRequest(
            Name: "",
            Calories: 120,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Crust_fails_validation_when_name_exceeds_max_length()
    {
        var validator = new UpdateCrustValidator();
        var longName = new string('A', Crust.MaxLength.Name + 1);
        var request = new UpdateCrustRequest(
            Name: longName,
            Calories: 120,
            IsActive: true
        );

        var result = await validator.TestValidateAsync(request);
        result.ShouldHaveValidationErrorFor(r => r.Name);
    }

    [Fact]
    public async Task Crust_fails_validation_when_calories_is_negative()
    {
        var validator = new UpdateCrustValidator();
        var request = new UpdateCrustRequest(
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
    public async Task Crust_is_not_updated_when_id_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new UpdateCrustRequest(
            Name: "New Name",
            Calories: 125,
            IsActive: true
        );

        Result<UpdateCrustResponse> result = await useCase.ExecuteAsync(999, request);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    #endregion

    #region Success outlines

    [Fact]
    public async Task Crust_is_updated_when_request_is_valid()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        var request = new UpdateCrustRequest(
            Name: "Updated Original",
            Calories: 130,
            IsActive: false
        );

        Result<UpdateCrustResponse> result = await useCase.ExecuteAsync(2, request);
        result.IsSuccess.ShouldBeTrue();
        result.Value.Name.ShouldBe("Updated Original");
        result.Value.Calories.ShouldBe(130);
        result.Value.IsActive.ShouldBeFalse();
    }

    #endregion

    private static UpdateCrustUseCase CreateUseCase(IDbContext dbContext) => new UpdateCrustUseCase(dbContext);
}
