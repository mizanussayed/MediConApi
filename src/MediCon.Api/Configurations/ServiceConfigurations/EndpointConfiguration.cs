using MediCon.Api.Configurations.Endpoints;

namespace MediCon.Api.Configurations.ServiceConfigurations;

public static class EndpointConfiguration
{
    public static IServiceCollection AddEndpointConfiguration(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<IApiAssemblyMarker>()
            .AddClasses(classes => classes.AssignableTo<IEndpoint>())
            .AsImplementedInterfaces()
            );

        return services;
    }
}
