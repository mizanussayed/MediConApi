using MediCon.Core;
using MediCon.Core.Configurations.Injector;
using MediCon.Infrastructure;

namespace MediCon.Api.Configurations.ServiceInjectors;

public static class InjectServices
{
    public static IServiceCollection AddInjectServices(this IServiceCollection services)
    {
        var injectServices = new ServiceCollection();

        injectServices.Scan(scan =>
            scan.FromAssembliesOf(typeof(IApiAssemblyMarker), typeof(ICoreAssemblyMarker), typeof(IInfrastructureAssemblyMarker))
            .AddClasses(classes => classes.AssignableTo<IInjectServices>())
            .AsImplementedInterfaces()
            );

        var serviceProvider = injectServices.BuildServiceProvider();
        foreach (var item in serviceProvider.GetRequiredService<IEnumerable<IInjectServices>>())
        {
            item.Configure(services);
        }

        return services;
    }
}
