using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Doctors.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Doctors;

internal sealed class DoctorDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<DoctorService>();
    }
}