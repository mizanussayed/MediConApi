using MediCon.Core.Features.RefreshTokens.Entity;
using MediCon.Core.Features.RefreshTokens.Repository;
using MediCon.Infrastructure.Configurations.DbContexts;

using Lib.DBAccess.Builder;
using Lib.DBAccess.NonQueries;
using Lib.DBAccess.Queries;
using Lib.ErrorOr;

namespace MediCon.Infrastructure.Repositories.RefreshTokens;

internal class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly OracleCFDBDbContext _dbContext;

    public RefreshTokenRepository(OracleCFDBDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<bool>> CreateRefreshTokenAsync(string token, DateTime expireTime, long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddVarchar2Parameter("P_TOKEN", token)
            .AddDateTimeParameter("P_EXPIRE_TIME", expireTime)
            .AddLongParameter("P_USER_ID", userId)
            .Build();

        var dbResult = await _dbContext.ExecuteNonQueryAsync(
            RefreshTokenProcedureNames.CREATE_REFRESH_TOKEN,
            parameters,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }

        return true;
    }

    public async Task<ErrorOr<RefreshToken>> GetRefreshTokenByIdAsync(long id, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddLongParameter("P_ID", id)
            .BuildForQuery();

        var dbResult = await _dbContext.QueryFirstAsync<RefreshToken>(
            RefreshTokenProcedureNames.GET_REFRESH_TOKEN_BY_ID,
            parameters,
            RefreshToken.MapFromDbWithReader,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }
        if (dbResult.Data is null)
        {
            return Error.Failure("Can not get data");
        }

        return dbResult.Data;
    }

    public async Task<ErrorOr<RefreshToken>> GetRefreshTokenByTokenAsync(string token, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddVarchar2Parameter("P_TOKEN", token)
            .BuildForQuery();

        var dbResult = await _dbContext.QueryFirstAsync<RefreshToken>(
            RefreshTokenProcedureNames.GET_REFRESH_TOKEN_BY_TOKEN,
            parameters,
            RefreshToken.MapFromDbWithReader,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }
        if (dbResult.Data is null)
        {
            return Error.Failure("Can not get data");
        }

        return dbResult.Data;
    }

    public async Task<ErrorOr<bool>> UpdateRefreshTokenAsync(long id, string token, DateTime expireTime, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddLongParameter("P_ID", id)
            .AddVarchar2Parameter("P_TOKEN", token)
            .AddDateTimeParameter("P_EXPIRE_TIME", expireTime)
            .Build();

        var dbResult = await _dbContext.ExecuteNonQueryAsync(
            RefreshTokenProcedureNames.UPDATE_REFRESH_TOKEN,
            parameters,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }

        return true;
    }

    public async Task<ErrorOr<bool>> DeleteRefreshTokenAsync(long id, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddLongParameter("P_ID", id)
            .Build();

        var dbResult = await _dbContext.ExecuteNonQueryAsync(
            RefreshTokenProcedureNames.DELETE_REFRESH_TOKEN,
            parameters,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }

        return true;
    }
}
