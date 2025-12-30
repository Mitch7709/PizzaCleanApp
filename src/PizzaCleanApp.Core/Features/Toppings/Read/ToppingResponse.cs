using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Toppings.Read;

public record ToppingResponse(
    long Id,
    string Name,
    decimal Price,
    int Calories,
    ToppingCategory CategoryType,
    bool IsActive
);
