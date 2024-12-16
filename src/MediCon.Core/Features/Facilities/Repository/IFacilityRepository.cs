using Lib.ErrorOr;

using MediCon.Core.Features.Facilities.Entity;

namespace MediCon.Core.Features.Facilities.Repository;

public interface IFacilityRepository
{
    Task<ErrorOr<IEnumerable<Facility>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Facility>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Facility entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Facility entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}