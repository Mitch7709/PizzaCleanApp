using PizzaCleanApp.Core.Features.Pizzas.Create;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.UnitTests.Pizzas;

public class CreatePizzaUseCaseTests
{
    [Fact]
    public async Task Pizza_is_not_created_when_name_already_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var createPizzaUseCase = CreateUseCase(context);
        var request = new CreatePizzaRequest
        (
            Name: "Margherita",
            Description: "Classic pizza with tomato sauce, mozzarella, and basil.",
            BasePrice: 8.99m,
            ToppingIds: new long[] { 1, 2 }
        );
        
        Result<CreatePizzaResponse> result = await createPizzaUseCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.Conflict);
    }

    private static CreatePizzaUseCase CreateUseCase(IDbContext dbContext)
        => new CreatePizzaUseCase(dbContext);
}
