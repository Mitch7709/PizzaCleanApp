using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Pizzas.Update;

public record UpdatePizzaRequest(
    long Id,
    string Name,
    string Description,
    decimal BasePrice
);

public record UpdatePizzaResponse(
    long Id,
    string Name,
    string Description
);
