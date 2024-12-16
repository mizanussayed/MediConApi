using MediCon.WebUI.Configurations.Auth;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Configurations.Settings;

namespace MediCon.WebUI.Configurations.ServiceConfigurations;

public static class WebServicesConfiguration
{
    public static IServiceCollection AddWebServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Http Client
        services.AddTransient<HttpClientTokenHandler>();

        var apiUrlSettings = ApiUrlSettings.Get(configuration);
        services.AddHttpClient(HttpClientKey.ApiNoAuth, client => client.BaseAddress = new Uri(apiUrlSettings.ApiBaseUrl));
        services.AddHttpClient(HttpClientKey.ApiAuth, client => client.BaseAddress = new Uri(apiUrlSettings.ApiBaseUrl)).AddHttpMessageHandler<HttpClientTokenHandler>();

        // Session
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(15);
            options.Cookie.Name = "_session_";
            options.Cookie.HttpOnly = true;
        });

        // Service Injectors
        services
            .AddInjectServices()
            .AddInjectServicesWithConfiguration(configuration);

        // Auth
        services.AddAuthenticationConfiguration(configuration);

        // Others
        services.AddHttpContextAccessor();

        // Other Services
        services.AddSingleton<IDateTimeHelper, DateTimeHelper>();

        return services;
    }
}
