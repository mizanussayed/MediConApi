using Lib.ErrorOr;

using MediCon.Core.Features.Countries.Entity;

namespace MediCon.Core.Features.Countries.Repository;

public interface ICountryRepository
{
    Task<ErrorOr<IEnumerable<Country>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Country>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Country entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Country entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}