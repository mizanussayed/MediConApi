using MediCon.Core.Configurations.Injector;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Configurations.ServiceConfigurations;

public class InfrastructureConfigurations : IInjectServicesWithConfiguration
{
    public void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseConfiguration(configuration);
    }
}
