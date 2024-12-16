using FluentValidation;

using MediCon.Core.Features.Specialities.Model;

namespace MediCon.Core.Specialities.Validators;

public sealed class SpecialityRequestModelValidator : AbstractValidator<SpecialityRequestModel>
{
    public SpecialityRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}