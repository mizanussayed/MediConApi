using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.ServiceTypes.Model;
using MediCon.Core.Features.ServiceTypes.Service;
using MediCon.Core.Features.ServiceTypes.Validator;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Features.ServiceTypes;

public class ServiceTypeDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<ServiceTypeService>();
        services.AddScoped<IValidator<ServiceTypeRequestModel>, ServiceTypeRequestModelValidator>();
    }
}
