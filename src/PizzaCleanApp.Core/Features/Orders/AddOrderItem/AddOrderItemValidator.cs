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
            RuleFor(x => x.PizzaId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.SizeId).GreaterThan(0);
            RuleFor(x => x.CrustId).GreaterThan(0);
        }
    }
}
