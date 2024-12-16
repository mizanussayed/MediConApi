using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Treatments.Service;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Core.Features.Treatments;

internal sealed class TreatmentDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<TreatmentService>();
    }
}