using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.FPackages.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.FPackages;

internal sealed class PackageDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IPackageRepository, PackageRepository>();
    }
}