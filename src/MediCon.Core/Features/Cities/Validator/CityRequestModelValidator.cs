using FluentValidation;

using MediCon.Core.Features.Cities.Model;

namespace MediCon.Core.Cities.Validators;

public sealed class CityRequestModelValidator : AbstractValidator<CityRequestModel>
{
    public CityRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}