using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.RefreshTokens.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Repositories.RefreshTokens;

public sealed class ConfigureRefreshServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}
