using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Hospitals.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Hospitals;

internal sealed class HospitalDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<HospitalService>();
    }
}