using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Countries.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Countries;

internal sealed class CountryDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<CountryService>();
    }
}