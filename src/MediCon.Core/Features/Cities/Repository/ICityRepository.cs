using Lib.ErrorOr;

using MediCon.Core.Features.Cities.Entity;

namespace MediCon.Core.Features.Cities.Repository;

public interface ICityRepository
{
    Task<ErrorOr<IEnumerable<City>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<City>> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(City entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(City entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}