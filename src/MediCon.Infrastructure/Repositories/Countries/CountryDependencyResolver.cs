using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Countries.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Countries;

internal sealed class CountryDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<ICountryRepository, CountryRepository>();
    }
}