using MediCon.Core.Features.Users.Entity;
using MediCon.Core.Features.Users.Repository;
using MediCon.Infrastructure.Configurations.DbContexts;

using Lib.DBAccess.Builder;
using Lib.DBAccess.Queries;
using Lib.ErrorOr;

namespace MediCon.Infrastructure.Repository.Users;

public class UserRepository : IUserRepository
{
    private readonly OracleDMSPhaseFourDbContext _oracleDMSPhaseFour;

    public UserRepository(OracleDMSPhaseFourDbContext oracleDMSPhaseFour)
    {
        _oracleDMSPhaseFour = oracleDMSPhaseFour;
    }

    public async Task<ErrorOr<User>> GetUserInformation(
        string userName,
        string password,
        string? accountNumber,
        CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddVarchar2Parameter("p_USERNAME", userName)
            .AddVarchar2Parameter("p_PASSWORD", password)
            .AddVarchar2Parameter("p_ACCOUNT_NUMBER", accountNumber)
            .BuildForQuery();

        var dbResult = await _oracleDMSPhaseFour.QueryFirstAsync<User>(
            UserProcedureNames.MFS_USER_AUTHENTICATION,
            parameters,
            User.MapFromDbWithDataRow,
            cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }
        if (dbResult.Data is null)
        {
            return Error.Failure("Can not retrieve user information");
        }

        return dbResult.Data;
    }

    public async Task<ErrorOr<UserLoginInformation>> GetUserLoginInformation(long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddLongParameter("P_ID", userId)
            .BuildForQuery();

        var dbResult = await _oracleDMSPhaseFour.QueryFirstAsync<UserLoginInformation>(
            UserProcedureNames.GET_USER_LOGIN_INFORMATION,
            parameters,
            UserLoginInformation.MapFromDbWithReader,
            cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }
        if (dbResult.Data is null)
        {
            return Error.Failure("Can not retrieve user information");
        }

        return dbResult.Data;
    }
}
