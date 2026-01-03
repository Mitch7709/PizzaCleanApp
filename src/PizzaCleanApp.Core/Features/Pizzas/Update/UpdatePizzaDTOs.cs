using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Pizzas.Update;

public record UpdatePizzaRequest(
    string Name,
    string Description,
    decimal BasePrice,
    bool IsActive = true,
    IReadOnlyCollection<long>? ToppingIds = null
);

public record UpdatePizzaResponse(
    long Id,
    string Name,
    string Description,
    decimal BasePrice,
    bool IsActive = true,
    IReadOnlyCollection<long>? ToppingIds = null
);
