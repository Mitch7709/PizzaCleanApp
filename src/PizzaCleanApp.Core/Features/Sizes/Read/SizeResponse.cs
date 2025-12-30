namespace PizzaCleanApp.Core.Features.Sizes.Read;

public record SizeResponse(
    long Id,
    string Name,
    decimal Price,
    int Calories
);
