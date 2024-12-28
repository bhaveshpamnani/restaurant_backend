using FluentValidation;
using restaurant_backend.Model;

namespace restaurant_backend.ValidationModel;

public class UserRoleValidationModel : AbstractValidator<UserModel>
{
    public UserRoleValidationModel()
    {
    }
}