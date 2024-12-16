using MediCon.Core.Features.Operators.Entity;

using Lib.ErrorOr;

namespace MediCon.Core.Features.Operators.Repository;

public interface IOperatorRepository
{
    Task<ErrorOr<IEnumerable<Operator>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Operator?>> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> CreateAsync(Operator entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> UpdateAsync(Operator entity, long id, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> DeleteAsync(long id, long userId, CancellationToken cancellationToken);
}