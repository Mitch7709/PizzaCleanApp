using PizzaCleanApp.Core.Features.Pizzas.Update;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Pizzas
{
    public class UpdatePizzaUseCaseTests
    {
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

        [Fact]
        public async Task Pizza_is_not_updated_when_name_is_empty()
        {
            using var builder = new DBBuilder();
            var context = builder.CreateDBContext();
            var updatePizzaUseCase = CreateUseCase(context);
            var request = new UpdatePizzaRequest
            (
                Name: "",
                Description: "Description with empty name.",
                BasePrice: 9.99m,
                IsActive: true
            );
            Result<UpdatePizzaResponse> result = await updatePizzaUseCase.ExecuteAsync(1, request);
            result.IsSuccess.ShouldBeFalse();
            result.ErrorType.ShouldBe(ErrorType.ValidationError);
        }


        private static UpdatePizzaUseCase CreateUseCase(IDbContext dbContext) => new UpdatePizzaUseCase(dbContext);
    }
}
