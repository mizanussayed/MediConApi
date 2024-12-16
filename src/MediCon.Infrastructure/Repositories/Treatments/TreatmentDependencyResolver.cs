using Microsoft.Extensions.DependencyInjection;

using MediCon.Core.Features.Treatments.Repository;
using MediCon.Core.Configurations.Injector;

namespace MediCon.Infrastructure.Repositories.Treatments;

internal sealed class TreatmentDependencyResolver : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<ITreatmentRepository, TreatmentRepository>();
    }
}