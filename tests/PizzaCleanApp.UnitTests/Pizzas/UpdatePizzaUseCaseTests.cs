using FluentValidation.TestHelper;
using PizzaCleanApp.Core.Features.Pizzas.Update;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Pizzas
{
    public class UpdatePizzaUseCaseTests
    {
        #region Validation outlines

        [Fact]
        public async Task Pizza_fails_validation_when_name_is_empty()
        {
            var validator = new UpdatePizzaValidator();
            var request = new UpdatePizzaRequest
            (
                Name: "",
                Description: "Description with empty name.",
                BasePrice: 9.99m,
                IsActive: true
            );
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(r => r.Name);
        }

        [Fact]
        public async Task Pizza_fails_validation_when_name_exceeds_max_length()
        {
            var validator = new UpdatePizzaValidator();
            var longName = new string('A', 101);
            var request = new UpdatePizzaRequest
            (
                Name: longName,
                Description: "Description with long name.",
                BasePrice: 9.99m,
                IsActive: true
            );
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(r => r.Name);
        }

        [Fact]
        public async Task Pizza_fails_validation_when_baseprice_is_less_than_0()
        {
            var validator = new UpdatePizzaValidator();
            var request = new UpdatePizzaRequest
            (
                Name: "Valid Name",
                Description: "",
                BasePrice: -9.99m,
                IsActive: true
            );
            var result = await validator.TestValidateAsync(request);
            result.ShouldHaveValidationErrorFor(r => r.BasePrice);
        }

        #endregion

        #region Failure Outlines

        [Fact]
        public async Task Pizza_is_not_updated_when_pizza_does_not_exist()
        {
            using var builder = new DBBuilder();
            var context = builder.CreateDBContext();
            var updatePizzaUseCase = CreateUseCase(context);
            var request = new UpdatePizzaRequest
            (
                Name: "NonExistentPizza",
                Description: "This pizza does not exist.",
                BasePrice: 10.99m,
                IsActive: true
            );
            Result<UpdatePizzaResponse> result = await updatePizzaUseCase.ExecuteAsync(999, request);
            result.IsSuccess.ShouldBeFalse();
            result.ErrorType.ShouldBe(ErrorType.NotFound);
        }

        [Fact]
        public async Task Pizza_is_not_updated_when_request_is_null()
        {   
            using var builder = new DBBuilder();
            var context = builder.CreateDBContext();
            var updatePizzaUseCase = CreateUseCase(context);

            Result<UpdatePizzaResponse> result = await updatePizzaUseCase.ExecuteAsync(1, null!);
            result.IsSuccess.ShouldBeFalse();
            result.ErrorType.ShouldBe(ErrorType.ValidationError);
        }

        #endregion

        #region Success Outlines

        [Fact]
        public async Task Pizza_is_updated_when_request_is_valid()
        {
            using var builder = new DBBuilder();
            var context = builder.CreateDBContext();
            var updatePizzaUseCase = CreateUseCase(context);
            var request = new UpdatePizzaRequest
            (
                Name: "UpdatedMargherita",
                Description: "Updated description for Margherita.",
                BasePrice: 11.99m,
                IsActive: false
            );
            Result<UpdatePizzaResponse> result = await updatePizzaUseCase.ExecuteAsync(1, request);
            result.IsSuccess.ShouldBeTrue();
            result.Value.Name.ShouldBe("UpdatedMargherita");
            result.Value.Description.ShouldBe("Updated description for Margherita.");
            result.Value.BasePrice.ShouldBe(11.99m);
            result.Value.IsActive.ShouldBeFalse();
        }

        #endregion

        private static UpdatePizzaUseCase CreateUseCase(IDbContext dbContext) => new UpdatePizzaUseCase(dbContext);
    }
}
