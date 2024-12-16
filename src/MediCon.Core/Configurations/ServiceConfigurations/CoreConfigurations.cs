using MediCon.Core.Configurations.Injector;
using MediCon.Core.Configurations.UnitOfWorks;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Configurations.ServiceConfigurations;

public class CoreConfigurations : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services
            .AddHelperConfiguration()
            .AddMapperConfigurations();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
