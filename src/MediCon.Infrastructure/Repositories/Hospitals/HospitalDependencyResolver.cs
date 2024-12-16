using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Hospitals.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Hospitals;

internal sealed class HospitalDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IHospitalRepository, HospitalRepository>();
    }
}