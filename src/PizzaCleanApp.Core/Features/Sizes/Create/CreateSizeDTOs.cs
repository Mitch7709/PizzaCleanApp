namespace PizzaCleanApp.Core.Features.Sizes.Create;

public record CreateSizeRequest(
    string Name,
    decimal Price,
    int Calories
);

public record CreateSizeResponse(
    long Id,
    string Name,
    decimal Price,
    int Calories
);
