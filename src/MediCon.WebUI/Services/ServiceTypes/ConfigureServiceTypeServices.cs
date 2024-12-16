using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Services.ServiceTypes.Services;

namespace MediCon.WebUI.Services.ServiceTypes;

public class ConfigureServiceTypeServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IServiceTypeService, ServiceTypeService>();
    }
}
