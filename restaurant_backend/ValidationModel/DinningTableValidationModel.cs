using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class DinningTableValidationModel : AbstractValidator<DinningTableModel>
{
    public DinningTableValidationModel()
    {
        RuleFor(d => d.TableCode).NotEmpty().NotNull().WithMessage("Table code is required");
        RuleFor(d => d.AvailabilityStatus).NotEmpty().NotNull().WithMessage("Availability_Status is required");
        RuleFor(d => d.PersonCount).NotEmpty().NotNull().WithMessage("PersonCount is required")
            .GreaterThan(0).WithMessage("Person Count should be greater than 0");
    }
}