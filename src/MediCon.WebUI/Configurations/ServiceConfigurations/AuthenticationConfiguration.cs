using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Controllers.Error;
using MediCon.WebUI.Controllers.Home;

using Microsoft.AspNetCore.Authentication.Cookies;

namespace MediCon.WebUI.Configurations.ServiceConfigurations;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = CookieKey.Auth;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = $"/{nameof(ErrorController).GetControllerName()}/{nameof(ErrorController.Forbidden)}";
                options.LoginPath = $"/{nameof(HomeController).GetControllerName()}/{nameof(HomeController.Login)}";
            });

        return services;
    }
}
