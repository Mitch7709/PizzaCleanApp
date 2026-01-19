using PizzaCleanApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Users.Register;

public class RegisterUseCase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public RegisterUseCase(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<Result<RegisterResponse>> ExecuteAsync(RegisterRequest request)
    {
        var existingUser = await _userService.FindByEmail(request.Email);
        if (existingUser != null)
        {
            return Result.Failure(ErrorType.ValidationError, "User already exists with this email.");
        }

        var user = new AppUser(request.Email, request.FirstName, request.LastName);

        var result = await _userService.Register(user, request.Password);
        if (result.IsFailure)
        {
            return Result.Failure(result.ErrorType.Value, result.ErrorMessage);
        }

        var token = await _tokenService.GenerateToken(user);

        return new RegisterResponse(user.Id, token);
    }
}
