using MediCon.Api.Configurations.Settings;
using MediCon.Core.Configurations.Settings;

namespace MediCon.Api.Configurations.ServiceConfigurations;

public static class SettingsConfiguration
{
    public static IServiceCollection AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.Configure<FileUploadSettings>(configuration.GetSection(FileUploadSettings.SectionName));

        return services;
    }
}
