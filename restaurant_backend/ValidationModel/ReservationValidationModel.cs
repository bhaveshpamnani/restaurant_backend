using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class ReservationValidationModel : AbstractValidator<ReservationModel>
{
    public ReservationValidationModel()
    {
        RuleFor(r => r.PersonCount).NotNull().NotEmpty().WithMessage("Person Count is required")
            .GreaterThan(0).WithMessage("Person Count should be greater than 0");
        RuleFor(r => r.BookDate).NotEmpty().NotNull().WithMessage("Book date is required");
        RuleFor(r => r.BookTime).NotEmpty().NotNull().WithMessage("Book Time is required");
    }
}