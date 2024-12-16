using FluentValidation;

using MediCon.Core.Features.Hospitals.Model;

namespace MediCon.Core.Hospitals.Validators;

public sealed class HospitalRequestModelValidator : AbstractValidator<HospitalRequestModel>
{
    public HospitalRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}