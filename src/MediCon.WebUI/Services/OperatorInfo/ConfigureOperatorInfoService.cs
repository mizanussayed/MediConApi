using MediCon.WebUI.Configurations.ServiceInjectors;
using MediCon.WebUI.Services.OperatorInfo.Services;
using MediCon.WebUI.Services.ServiceTypes.Services;

namespace MediCon.WebUI.Services.OperatorInfo
{
    public class ConfigureOperatorInfoService : IInjectServices
    {
        public void Configure(IServiceCollection services)
        {
            services.AddScoped<IOperatorInfoService, OperatorInfoService>();
        }
    }
}