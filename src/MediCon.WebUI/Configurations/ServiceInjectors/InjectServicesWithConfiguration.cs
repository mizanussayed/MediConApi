using MediCon.WebUI;

namespace MediCon.WebUI.Configurations.ServiceInjectors;

public static class InjectServicesWithConfiguration
{
    public static IServiceCollection AddInjectServicesWithConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var injectServices = new ServiceCollection();

        injectServices.Scan(scan =>
            scan.FromAssembliesOf(typeof(IWebUIAssemblyMarker))
            .AddClasses(classes => classes.AssignableTo<IInjectServicesWithConfiguration>())
            .AsImplementedInterfaces()
            );

        var serviceProvider = injectServices.BuildServiceProvider();
        foreach (var item in serviceProvider.GetRequiredService<IEnumerable<IInjectServicesWithConfiguration>>())
        {
            item.Configure(services, configuration);
        }

        return services;
    }
}
