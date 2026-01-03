using PizzaCleanApp.Core.Features.Crusts.Delete;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;
using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.UnitTests.Crusts;

public class DeleteCrustUseCaseTests
{
    [Fact]
    public async Task Crust_is_not_deleted_when_id_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        Result result = await useCase.ExecuteAsync(999);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    [Fact]
    public async Task Crust_is_deleted_when_id_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);

        Result result = await useCase.ExecuteAsync(1);
        result.IsSuccess.ShouldBeTrue();
    }

    private static DeleteCrustUseCase CreateUseCase(IDbContext context)
    {
        return new DeleteCrustUseCase(context);
    }
}
