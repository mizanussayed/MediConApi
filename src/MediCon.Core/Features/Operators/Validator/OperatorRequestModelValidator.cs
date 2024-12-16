using MediCon.Core.Features.Operators.Model;

using FluentValidation;

namespace MediCon.Core.Operators.Validators;

public sealed class OperatorRequestModelValidator : AbstractValidator<OperatorRequestModel>
{
    public OperatorRequestModelValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Name is required");

        RuleFor(x => x.BinNumber)
             .NotEmpty()
             .WithMessage("Bin number is required");

        RuleFor(x => x.TinNumber)
            .NotEmpty()
            .WithMessage("Tin number is required");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Address is required");
    }
}