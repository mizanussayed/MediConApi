using MediCon.Core.Features.Users.Model;

using FluentValidation;

namespace MediCon.Core.Features.Users.Validators;

public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(2).WithMessage("Minimum length must be at least 2 characters");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(2).WithMessage("Minimum length must be at least 2 characters");
    }
}
