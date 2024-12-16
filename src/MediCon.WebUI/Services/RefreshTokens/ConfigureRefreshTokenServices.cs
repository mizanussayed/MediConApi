using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Services.RefreshTokens.Services;

namespace MediCon.WebUI.Services.RefreshTokens;

public class ConfigureRefreshTokenServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
    }
}
