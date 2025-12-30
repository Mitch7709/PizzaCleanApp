namespace PizzaCleanApp.Core.Features.Crusts.Update;

public record UpdateCrustRequest(
    string Name,
    int Calories,
    bool IsActive
);

public record UpdateCrustResponse(
    long Id,
    string Name,
    int Calories,
    bool IsActive
);
