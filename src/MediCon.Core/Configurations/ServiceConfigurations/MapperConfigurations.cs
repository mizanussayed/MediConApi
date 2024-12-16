using System.Reflection;

using MediCon.Core.Configurations.Pagination;

using Mapster;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Configurations.ServiceConfigurations;

public static class MapperConfigurations
{
    public static IServiceCollection AddMapperConfigurations(this IServiceCollection services)
    {
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        mapsterConfig.Scan(Assembly.Load(typeof(ICoreAssemblyMarker).Assembly.FullName!));

        return services;
    }
}
