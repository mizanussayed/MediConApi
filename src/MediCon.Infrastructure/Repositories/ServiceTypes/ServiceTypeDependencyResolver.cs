using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.ServiceTypes.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Repositories.ServiceTypes;

public class ServiceTypeDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
    }
}
