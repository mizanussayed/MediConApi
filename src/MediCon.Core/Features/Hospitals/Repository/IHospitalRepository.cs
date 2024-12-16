using Lib.ErrorOr;

using MediCon.Core.Features.Hospitals.Entity;

namespace MediCon.Core.Features.Hospitals.Repository;

public interface IHospitalRepository
{
    Task<ErrorOr<IEnumerable<Hospital>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Hospital>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Hospital entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Hospital entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}