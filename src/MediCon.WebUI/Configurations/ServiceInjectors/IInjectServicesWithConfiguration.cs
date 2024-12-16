namespace MediCon.WebUI.Configurations.ServiceInjectors;

public interface IInjectServicesWithConfiguration
{
    void Configure(IServiceCollection services, IConfiguration configuration);
}
