using PizzaCleanApp.Core.Models;

namespace PizzaCleanApp.Core.Features.Users.Login;

public class LoginUseCase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<Result<LoginResponse>> ExecuteAsync(LoginRequest request)
    {
        var user = await _userService.Login(request.Email, request.Password);
        if (user is null)
        {
            return Result.Failure(ErrorType.ValidationError, "Invalid email or password.");
        }

        var token = await _tokenService.GenerateToken(user);
        return new LoginResponse(user.Id, token);
    }
}
