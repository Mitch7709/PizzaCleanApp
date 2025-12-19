using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Pizzas.Read;

public record PizzaResponse(long Id, string Name, string Description, decimal BasePrice, bool IsActive);
