using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Operators.Model;
using MediCon.Core.Features.Operators.Service;
using MediCon.Core.Operators.Validators;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Features.Operators;

internal sealed class OperatorDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<OperatorService>();
        services.AddScoped<IValidator<OperatorRequestModel>, OperatorRequestModelValidator>();
    }
}