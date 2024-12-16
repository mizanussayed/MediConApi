using MediCon.Api.Configurations.Exceptions;
using MediCon.Api.Configurations.ServiceInjectors;
using MediCon.Core.Configurations.Constants;
using MediCon.Core.Configurations.Settings.ApiUrl;

namespace MediCon.Api.Configurations.ServiceConfigurations;

public static class ApiConfigurations
{
    public static IServiceCollection AddApiConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddInjectServices()
            .AddInjectServicesWithConfiguration(configuration)
            .AddSwaggerConfiguration()
            .AddEndpointConfiguration()
            .AddAuthConfiguration(configuration)
            .AddSettingsConfiguration(configuration)
            .AddHelperConfiguration();

        // Exception Handlers
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        // Http Client
        SetupHttpClient(services, configuration);

        return services;
    }

    private static void SetupHttpClient(IServiceCollection services, IConfiguration configuration)
    {
        var apiUrls = configuration.GetSection(ApiUrlSettings.SectionName).Get<ApiUrlSettings>() ?? throw new Exception($"Could not load {ApiUrlSettings.SectionName} from appsettings");

        // HTTP Client
        services.AddHttpClient(
            HttpClientKey.JsonPlaceHolder,
            client => client.BaseAddress = new Uri(apiUrls.JsonPlaceHolder ?? throw new Exception("ApiUrls:JsonPlaceHolder could not be loaded")));

        services.AddHttpClient(
            HttpClientKey.LoginApi,
            client => client.BaseAddress = new Uri(apiUrls.LoginApi?.Url ?? throw new Exception("ApiUrls:LoginApi:Url could not be loaded")));
    }
}
