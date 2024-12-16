using Lib.ErrorOr;

using MediCon.Core.Features.FPackages.Entity;

namespace MediCon.Core.Features.FPackages.Repository;

public interface IPackageRepository
{
    Task<ErrorOr<IEnumerable<Package>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Package>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Package entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Package entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}