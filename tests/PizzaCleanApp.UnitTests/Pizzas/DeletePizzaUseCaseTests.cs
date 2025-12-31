using PizzaCleanApp.Core.Features.Pizzas.Delete;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Pizzas;

public class DeletePizzaUseCaseTests
{
    [Fact]
    public async Task Pizza_is_not_deleted_when_id_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var deletePizzaUseCase = CreateUseCase(context);
        Result result = await deletePizzaUseCase.ExecuteAsync(999);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    private static DeletePizzaUseCase CreateUseCase(IDbContext context)
    {
        return new DeletePizzaUseCase(context);
    }
}
