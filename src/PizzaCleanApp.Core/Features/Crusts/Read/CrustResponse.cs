namespace PizzaCleanApp.Core.Features.Crusts.Read;

public record CrustResponse(
    long Id,
    string Name,
    int Calories,
    bool IsActive
);
