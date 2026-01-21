using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Shared
{
    public interface IUserContext
    {
        bool IsAuthenticated();
        string? GetUserId();
    }
}
