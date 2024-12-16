using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Facilities.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Facilities;

internal sealed class FacilityDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IFacilityRepository, FacilityRepository>();
    }
}