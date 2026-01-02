using PizzaCleanApp.Core.Features.Sizes.Delete;
using PizzaCleanApp.Core.Models;
using PizzaCleanApp.Core.Shared;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Sizes;

public class DeleteSizeUseCaseTests
{
    [Fact]
    public async Task Size_is_not_deleted_when_id_does_not_exist()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);
        Result result = await useCase.ExecuteAsync(999);
        result.IsSuccess.ShouldBeFalse();
        result.ErrorType.ShouldBe(ErrorType.NotFound);
    }

    [Fact]
    public async Task Size_is_deleted_when_id_exists()
    {
        using var builder = new DBBuilder();
        var context = builder.CreateDBContext();
        var useCase = CreateUseCase(context);

        Result result = await useCase.ExecuteAsync(1);
        result.IsSuccess.ShouldBeTrue();
    }

    private static DeleteSizeUseCase CreateUseCase(IDbContext context)
    {
        return new DeleteSizeUseCase(context);
    }
}
