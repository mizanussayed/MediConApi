using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Users.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Infrastructure.Repositories.Users;

public class ConfigureUserServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
