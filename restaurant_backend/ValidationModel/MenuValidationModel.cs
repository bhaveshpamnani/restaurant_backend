using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class MenuValidationModel : AbstractValidator<MenuModel>
{
    public MenuValidationModel()
    {
        RuleFor(m => m.CategoryID).NotEmpty().NotNull().WithMessage("Category must required");
        RuleFor(m => m.DishName).NotEmpty().NotNull().WithMessage("Dish name must required");
        RuleFor(m => m.Description).NotEmpty().NotNull().WithMessage("Description must required");
        RuleFor(m => m.Price).NotEmpty().NotNull().WithMessage("Price must required");
        RuleFor(m => m.ImageURL).NotEmpty().NotNull().WithMessage("ImageUrl must required");
        RuleFor(m => m.AvailabilityStatus).NotEmpty().NotNull().WithMessage("AvailabilityStatus must required");
    }
}