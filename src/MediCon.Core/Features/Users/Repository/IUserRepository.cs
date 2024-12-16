using MediCon.Core.Features.Users.Entity;

using Lib.ErrorOr;

namespace MediCon.Core.Features.Users.Repository;

public interface IUserRepository
{
    Task<ErrorOr<User>> GetUserInformation(
        string userName,
        string password,
        string? accountNumber, CancellationToken cancellationToken);
    Task<ErrorOr<UserLoginInformation>> GetUserLoginInformation(long userId, CancellationToken cancellationToken);
}
