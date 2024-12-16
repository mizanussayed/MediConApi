using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Tests.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Repositories.Tests;

public class ConfigureTestServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<ITestRepository, TestRepository>();
    }
}
