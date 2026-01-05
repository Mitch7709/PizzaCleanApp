using PizzaCleanApp.Core.Features.Crusts.Create;
using PizzaCleanApp.Core.Features.Orders.AddOrderItem;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.UnitTests.Orders;

public class AddOrderItemUseCaseTests
{
    [Fact]
    public async Task Create_new_order_when_orderitem_is_added()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);

        var request = new AddOrderItemRequest(
            PizzaId: 1,
            Quantity: 1,
            SizeId: 1,
            CrustId: 1,
            ToppingIds: new List<long> { 1, 2 }
        );

        await useCase.Execute(1, request);
    }

    private static AddOrderItemUseCase CreateUseCase(IDbContext dbContext)
        => new(dbContext);
}
