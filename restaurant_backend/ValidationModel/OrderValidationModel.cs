using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class OrderValidationModel: AbstractValidator<OrderModel>
{
    public OrderValidationModel()
    {
        RuleFor(o => o.TableID).NotEmpty().NotNull().WithMessage("Table Code is required");
    }
}