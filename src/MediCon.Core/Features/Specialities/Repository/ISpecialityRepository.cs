using Lib.ErrorOr;

using MediCon.Core.Features.Specialities.Entity;

namespace MediCon.Core.Features.Specialities.Repository;

public interface ISpecialityRepository
{
    Task<ErrorOr<IEnumerable<Speciality>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Speciality>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Speciality entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Speciality entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}