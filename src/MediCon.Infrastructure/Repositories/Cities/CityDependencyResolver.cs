using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Cities.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Cities;

internal sealed class CityDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<ICityRepository, CityRepository>();
    }
}