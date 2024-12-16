using MediCon.Core.Features.RefreshTokens.Model;

using FluentValidation;

namespace MediCon.Core.Features.RefreshTokens.Validators;

public class NewRefreshTokenValidator : AbstractValidator<NewTokenRequestModel>
{
    public NewRefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
