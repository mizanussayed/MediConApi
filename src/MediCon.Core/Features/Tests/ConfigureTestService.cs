using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Tests.Service;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Features.Tests;

public class ConfigureTestService : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<TestService>();
    }
}
