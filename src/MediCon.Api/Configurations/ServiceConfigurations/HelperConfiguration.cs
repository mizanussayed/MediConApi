using MediCon.Api.Configurations.Helpers;
using MediCon.Core.Configurations.Helpers;
using MediCon.Core.Configurations.Settings;

namespace MediCon.Api.Configurations.ServiceConfigurations;

public static class HelperConfiguration
{
    public static void AddHelperConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenHelper, JwtTokenHelper>();
        services.AddScoped<FileHelper>();
        services.AddSingleton<ISettingsHelper, SettingsHelper>();
    }
}
