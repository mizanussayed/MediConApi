using MediCon.Core.Features.ServiceTypes.Entity;

using Lib.ErrorOr;

namespace MediCon.Core.Features.ServiceTypes.Repository;

public interface IServiceTypeRepository
{
    Task<ErrorOr<IEnumerable<ServiceType>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<ServiceType?>> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> CreateAsync(ServiceType entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> UpdateAsync(ServiceType entity, long id, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> DeleteAsync(long id, long userId, CancellationToken cancellationToken);
}