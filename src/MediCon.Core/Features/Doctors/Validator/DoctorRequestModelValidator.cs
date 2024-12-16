using FluentValidation;

using MediCon.Core.Features.Doctors.Model;

namespace MediCon.Core.Doctors.Validators;

public sealed class DoctorRequestModelValidator : AbstractValidator<DoctorRequestModel>
{
    public DoctorRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}