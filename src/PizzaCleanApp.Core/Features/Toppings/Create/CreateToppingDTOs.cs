using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Toppings.Create;

public record CreateToppingRequest(
    string Name,
    decimal Price,
    int Calories,
    ToppingCategory CategoryType,
    bool IsActive = true
);

public record CreateToppingResponse(
    long Id,
    string Name,
    decimal Price,
    int Calories,
    ToppingCategory CategoryType,
    bool IsActive
);
