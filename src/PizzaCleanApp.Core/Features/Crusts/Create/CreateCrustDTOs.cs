using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Crusts.Create;

public record CreateCrustRequest(
    string Name,
    int Calories,
    bool IsActive = true
);

public record CreateCrustResponse(
    long Id,
    string Name,
    int Calories,
    bool IsActive
);
