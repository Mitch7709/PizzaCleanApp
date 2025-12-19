using PizzaCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Pizzas.Create;

public record CreatePizzaRequest(
    string Name,
    string Description,
    decimal BasePrice,
    IReadOnlyCollection<long>? ToppingIds // optional; null treated as empty
);

public record CreatePizzaResponse(long Id, string Name, string Description);
