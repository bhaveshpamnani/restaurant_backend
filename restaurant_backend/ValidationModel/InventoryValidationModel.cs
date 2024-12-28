using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class InventoryValidationModel : AbstractValidator<InventoryModel>
{
    public InventoryValidationModel()
    {
        RuleFor(i => i.ItemName).NotEmpty().NotNull().WithMessage("Item Name is required");
        RuleFor(i => i.QuantityAvailable).NotEmpty().NotNull().WithMessage("QuantityAvailable is required");
        RuleFor(i => i.QuantityWanted).NotEmpty().NotNull().WithMessage("QuantityWanted is required");
    }
}