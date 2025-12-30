namespace PizzaCleanApp.Core.Features.Sizes.Update;

public record UpdateSizeRequest(
    string Name,
    decimal Price,
    int Calories
);

public record UpdateSizeResponse(
    long Id,
    string Name,
    decimal Price,
    int Calories
);
