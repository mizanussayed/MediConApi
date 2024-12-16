using Lib.ErrorOr;

using MediCon.Core.Features.Treatments.Entity;

namespace MediCon.Core.Features.Treatments.Repository;

public interface ITreatmentRepository
{
    Task<ErrorOr<IEnumerable<Treatment>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Treatment>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Treatment entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Treatment entity,  CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}