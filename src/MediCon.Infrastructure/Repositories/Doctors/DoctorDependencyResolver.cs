using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Doctors.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Doctors;

internal sealed class DoctorDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
    }
}