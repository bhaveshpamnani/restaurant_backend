using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class FeedbackValidationModel : AbstractValidator<FeedbackModel>
{
    public FeedbackValidationModel()
    {
        RuleFor(f => f.Description).NotEmpty().NotNull().WithMessage("Description is required");
        RuleFor(f => f.FeedbackCategoryID).NotEmpty().NotNull().WithMessage("Category is required");
    }
}