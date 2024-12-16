using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Configurations.Injector;

public interface IInjectServices
{
    void Configure(IServiceCollection services);
}
