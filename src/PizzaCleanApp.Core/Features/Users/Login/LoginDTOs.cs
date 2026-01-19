using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Users.Login;

public record LoginRequest(string Email, string Password);
public record LoginResponse(string UserId, string Token);
