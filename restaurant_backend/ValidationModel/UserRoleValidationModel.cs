using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class UserRoleValidationModel : AbstractValidator<UserModel>
{
    public UserRoleValidationModel()
    {
        RuleFor(u => u.UserName).NotEmpty().WithMessage("Name is required");
        RuleFor(u => u.UserName).MaximumLength(10).WithMessage("Name must be in length 10");
        RuleFor(u => u.UserEmail).NotEmpty().WithMessage("Email is required");
        RuleFor(u => u.UserEmail).EmailAddress().WithMessage("Email must be valid");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(5).WithMessage("Your password length must be at least 5.")
            .MaximumLength(10).WithMessage("Your password length must not exceed 10.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        RuleFor(u => u.Phone).NotEmpty().NotNull().WithMessage("Your Phone Number cannot be empty")
            .MaximumLength(10).WithMessage("Your password length must not exceed 10")
            .MinimumLength(10).WithMessage("Your password length must not least 10");

    }
}