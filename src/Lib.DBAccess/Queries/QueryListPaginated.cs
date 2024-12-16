using MediCon.Core.Configurations.Pagination;

using Lib.DBAccess.Contexts;
using Lib.DBAccess.Model;

using Oracle.ManagedDataAccess.Client;

using System.Data.Common;

namespace Lib.DBAccess.Queries;

public static class QueryListPaginated
{
    #region Oracle
    public static async Task<DatabaseQueryListPaginatedResponse<T>> QueryListPaginatedAsync<T>(
        this OracleDbContext dbContext,
        string procedureName,
        IEnumerable<OracleParameter> parameters,
        Action<DbDataReader, T> mapAction,
        (int PageNo, int PageSize) paginationRequest,
        CancellationToken cancellationToken,
        OracleTransaction? transaction = null)
        where T : class, IPaginationTotalItems, new()
    {
        var result = await dbContext.QueryListAsync(procedureName, parameters, mapAction, cancellationToken, transaction).ConfigureAwait(false);
        if (!result.Success)
        {
            return DatabaseQueryListPaginatedResponse.Failure<T>(result.ErrorMessage ?? string.Empty);
        }

        var totalItems = result.Data.FirstOrDefault()?.TotalItems ?? 0;
        return new DatabaseQueryListPaginatedResponse<T>
        {
            Success = result.Success,
            ErrorCode = result.ErrorCode,
            ErrorMessage = result.ErrorMessage,
            PaginatedData = new PaginationResult<T>(result.Data, paginationRequest.PageNo, paginationRequest.PageSize, totalItems),
        };
    }
    #endregion

    #region MySql
    #endregion
}
