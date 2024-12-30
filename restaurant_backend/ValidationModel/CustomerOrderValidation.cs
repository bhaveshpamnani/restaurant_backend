using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class CustomerOrderValidation : AbstractValidator<CustomerOrderModel>
{
    public CustomerOrderValidation()
    {
        RuleFor(c => c.TableID).NotNull().NotEmpty().WithMessage("Table code is required");
        RuleFor(c => c.MenuID).NotNull().NotEmpty().WithMessage("At least 1 item is required");
        RuleFor(c => c.Quantity).NotNull().NotEmpty().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");
        RuleFor(c => c.TotalAmount).NotNull().NotEmpty().WithMessage("Total amount is required")
            .GreaterThan(0).WithMessage("Total Amount is greater than 0");
        RuleFor(c => c.PaymentStatus).NotNull().NotEmpty().WithMessage("Payment status is required");
        RuleFor(c => c.OrderStatus).NotNull().NotEmpty().WithMessage("OrderStatus  is required");
    }
        
}