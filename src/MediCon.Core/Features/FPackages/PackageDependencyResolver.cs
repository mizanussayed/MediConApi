using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.FPackages.Service;

namespace MediCon.Core.Features.FPackages;

internal sealed class PackageDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<PackageService>();
    }
}