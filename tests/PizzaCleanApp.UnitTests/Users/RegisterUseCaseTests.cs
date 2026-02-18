using PizzaCleanApp.Core.Features.Users;
using PizzaCleanApp.Core.Features.Users.Register;
using PizzaCleanApp.UnitTests.TestSetup;
using Shouldly;

namespace PizzaCleanApp.UnitTests.Users;

public class RegisterUseCaseTests
{
    [Fact]
    public async Task RegisterUser_Succeeds_WithValidInput()
    {
        using var setup = new IdentityTestSetup();
        var useCase = CreateUseCase(setup.UserService, setup.TokenService);

        var request = new RegisterRequest(
            Email: "newuser@example.com",
            Password: "Password123",
            Role: "Admin",
            FirstName: "New",
            LastName: "User");

        var result = await useCase.ExecuteAsync(request);

        result.IsSuccess.ShouldBeTrue();
        result.Value.UserId.ShouldNotBeNullOrEmpty();

        var saved = await setup.UserManager.FindByEmailAsync("newuser@example.com");
        saved.ShouldNotBeNull();
        var roles = await setup.UserManager.GetRolesAsync(saved!);
        roles.ShouldContain(request.Role); // role applied by UserService.Register
    }


    private static RegisterUseCase CreateUseCase(IUserService userService, ITokenService tokenService)
    {
        return new RegisterUseCase(userService, tokenService);
    }
}
