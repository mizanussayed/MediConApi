using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Operators.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Repository.Operators;

internal sealed class OperatorDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IOperatorRepository, OperatorRepository>();
        services.AddScoped<IOperatorRepository, OperatorRepositorycopy>();
    }
}