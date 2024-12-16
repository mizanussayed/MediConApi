using MediCon.Infrastructure.Configurations.DbContexts;
using MediCon.Infrastructure.Configurations.Settings;

using Lib.DBAccess.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Configurations.ServiceConfigurations;

internal static class DatabaseConfiguration
{
    internal static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = DatabaseSettings.Get(configuration);

        services.AddScoped(options => new OracleCFDBDbContext(databaseSettings.OracleCFDB));
        services.AddScoped(options => new OracleDMSPhaseFourDbContext(databaseSettings.OracleDMSPhaseFour));
        services.AddScoped(options => new MySqlDbContext(databaseSettings.MySql));
        services.AddDbContext<OracleCFDBDbContext_copy>();
    }
}
