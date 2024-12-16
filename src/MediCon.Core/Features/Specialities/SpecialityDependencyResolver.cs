using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Specialities.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Specialities;

internal sealed class SpecialityDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<SpecialityService>();
    }
}