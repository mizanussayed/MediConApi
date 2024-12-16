using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Configurations.Injector;

public interface IInjectServicesWithConfiguration
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}
