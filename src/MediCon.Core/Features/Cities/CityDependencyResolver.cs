using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Cities.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Cities;

internal sealed class CityDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<CityService>();
    }
}