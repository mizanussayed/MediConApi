using MediCon.Core.Features.ServiceTypes.Model;

using FluentValidation;

namespace MediCon.Core.Features.ServiceTypes.Validator;

public class ServiceTypeRequestModelValidator : AbstractValidator<ServiceTypeRequestModel>
{
    public ServiceTypeRequestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}
