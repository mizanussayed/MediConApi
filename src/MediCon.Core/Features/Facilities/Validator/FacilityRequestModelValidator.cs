using FluentValidation;

using MediCon.Core.Features.Facilities.Model;

namespace MediCon.Core.Facilities.Validators;

public sealed class FacilityRequestModelValidator : AbstractValidator<FacilityRequestModel>
{
    public FacilityRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}