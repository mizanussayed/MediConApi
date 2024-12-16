using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Services.Users.Services;

namespace MediCon.WebUI.Services.Users;

public class ConfigureUserServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}
