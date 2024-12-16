using FluentValidation;

using MediCon.Core.Features.Countries.Model;

namespace MediCon.Core.Countries.Validators;

public sealed class CountryRequestModelValidator : AbstractValidator<CountryRequestModel>
{
    public CountryRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}