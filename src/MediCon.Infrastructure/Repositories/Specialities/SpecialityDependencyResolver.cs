using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Specialities.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Specialities;

internal sealed class SpecialityDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<ISpecialityRepository, SpecialityRepository>();
    }
}