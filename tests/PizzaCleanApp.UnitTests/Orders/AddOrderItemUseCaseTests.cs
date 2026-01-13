using PizzaCleanApp.Core.Features.Orders.AddOrderItem;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Orders;

public class AddOrderItemUseCaseTests
{
    #region Success Outlines

    [Fact]
    public async Task Create_new_order_when_orderitem_is_added()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);

        var request = new AddOrderItemRequest(
            OrderId: 1,
            PizzaId: 1,
            Quantity: 1,
            SizeId: 1,
            CrustId: 1,
            ToppingIds: new List<long> { 1, 2 }
        );

        Result<AddOrderItemResponse> result = await useCase.Execute(request);

        result.IsSuccess.ShouldBeTrue();
    }

    #endregion

    private static AddOrderItemUseCase CreateUseCase(IDbContext dbContext)
        => new(dbContext);
}
