using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCleanApp.Core.Features.Orders.AddOrderItem
{
    public class AddOrderItemValidator : AbstractValidator<AddOrderItemRequest>
    {
        public AddOrderItemValidator()
        {
            RuleFor(x => x.PizzaId).NotNull().GreaterThan(0);
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.SizeId).NotNull().GreaterThan(0);
            RuleFor(x => x.CrustId).NotNull().GreaterThan(0);
        }
    }
}
