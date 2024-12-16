using FluentValidation;

using MediCon.Core.Features.FPackages.Model;

namespace MediCon.Core.Packages.Validators;

public sealed class PackageRequestModelValidator : AbstractValidator<PackageRequestModel>
{
    public PackageRequestModelValidator()
    {
     //   RuleFor(x => x.BPartyCustomerName)
     //      .NotEmpty()
     //      .WithMessage("B Party Customer Name is required");
    }
}