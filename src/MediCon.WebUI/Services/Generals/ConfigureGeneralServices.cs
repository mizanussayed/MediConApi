using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Services.Generals.Services;

namespace MediCon.WebUI.Services.Generals;

public class ConfigureGeneralServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IGeneralService, GeneralService>();
    }
}
