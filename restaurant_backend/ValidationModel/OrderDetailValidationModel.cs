using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class OrderDetailValidationModel : AbstractValidator<OrderDetailModel>
{
    public OrderDetailValidationModel()
    {
        RuleFor(o=>o.MenuID).NotEmpty().NotNull().WithMessage("required onw menu item");
        RuleFor(o=>o.Quantity).NotEmpty().NotNull().WithMessage("required onw menu item")
        .GreaterThan(0).WithMessage("Menu item must have greater than 0");
    }
}