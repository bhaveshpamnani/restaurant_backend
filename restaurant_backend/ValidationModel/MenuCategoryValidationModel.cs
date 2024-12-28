using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class MenuCategoryValidationModel : AbstractValidator<MenuCategoryModel>
{
    public MenuCategoryValidationModel()
    {
        RuleFor(m => m.CategoryName).NotNull().NotEmpty().WithMessage("Category Name is required");
        RuleFor(m => m.Description).NotNull().NotEmpty().WithMessage("Description Name is required");
    }
}