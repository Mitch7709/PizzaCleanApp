using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Toppings.Update;

public record UpdateToppingRequest(
    string Name,
    decimal Price,
    int Calories,
    ToppingCategory CategoryType,
    bool IsActive
);

public record UpdateToppingResponse(
    long Id,
    string Name,
    decimal Price,
    int Calories,
    ToppingCategory CategoryType,
    bool IsActive
);
