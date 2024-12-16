using FluentValidation;

using MediCon.Core.Features.Treatments.Model;

namespace MediCon.Core.Treatments.Validators;

public sealed class TreatmentRequestModelValidator : AbstractValidator<TreatmentRequestModel>
{
    public TreatmentRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}