using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Facilities.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Facilities;

internal sealed class FacilityDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<FacilityService>();
    }
}