using MediCon.Core.Features.RefreshTokens.Entity;

using Lib.ErrorOr;

namespace MediCon.Core.Features.RefreshTokens.Repository;

public interface IRefreshTokenRepository
{
    Task<ErrorOr<bool>> CreateRefreshTokenAsync(string token, DateTime expireTime, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> DeleteRefreshTokenAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<RefreshToken>> GetRefreshTokenByIdAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<RefreshToken>> GetRefreshTokenByTokenAsync(string token, CancellationToken cancellationToken);
    Task<ErrorOr<bool>> UpdateRefreshTokenAsync(long id, string token, DateTime expireTime, CancellationToken cancellationToken);
}
