using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Users.Register;

public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
public record RegisterResponse(string UserId, string Token);
