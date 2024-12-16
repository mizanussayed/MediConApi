using System.Data;
using System.Data.Common;
using System.Globalization;

using Lib.DBAccess.Contexts;
using Lib.DBAccess.Helpers;
using Lib.DBAccess.Model;

using MySqlConnector;

using Oracle.ManagedDataAccess.Client;

namespace Lib.DBAccess.Queries;

public static class QueryScalar
{
    #region Oracle
    /// <summary>
    /// Gets a value type data. For single row and single column
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbContext"></param>
    /// <param name="procedureName"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public static Task<DatabaseQueryScalarResponse<T>> QueryScalarAsync<T>(
        this OracleDbContext dbContext,
        string procedureName,
        IEnumerable<OracleParameter> parameters,
        CancellationToken cancellationToken,
        OracleTransaction? transaction = null)
        where T : struct
    {
        var connection = dbContext.GetDbConnection();
        using var command = connection.CreateCommand();

        CommonHelper.SetupOracleCommandWithParameters(command, procedureName, parameters, transaction);

        return ExecuteQueryAsync<T>(connection, command, cancellationToken);
    }

    private static async Task<DatabaseQueryScalarResponse<T>> ExecuteQueryAsync<T>(
        DbConnection connection,
        OracleCommand command,
        CancellationToken cancellationToken)
        where T : struct
    {
        try
        {
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            }

            var result = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

            var errorMessage = command.Parameters[DefaultConstants.ErrorMessageParameter].Value?.ToString();
            _ = int.TryParse(command.Parameters[DefaultConstants.ErrorCodeParameter].Value?.ToString(), NumberStyles.None, provider: null, out var errorCode);

            return new DatabaseQueryScalarResponse<T>
            {
                Success = errorMessage?.Equals(DefaultConstants.SuccessfulMessage, StringComparison.Ordinal) == true,
                ErrorMessage = errorMessage ?? DefaultConstants.NoErrorMessage,
                ErrorCode = errorCode,
                Data = result is null ? default : (T)result,
            };
        }
        catch (Exception ex)
        {
            return DatabaseQueryScalarResponse.Failure<T>(ex.Message);
        }
    }
    #endregion

    #region MySql
    /// <summary>
    /// Gets a value type data. For single row and single column
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbContext"></param>
    /// <param name="procedureName"></param>
    /// <param name="parameters"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public static async Task<DatabaseQueryScalarResponse<T>> QueryScalarAsync<T>(
        this MySqlDbContext dbContext,
        string procedureName,
        IEnumerable<MySqlParameter> parameters,
        CancellationToken cancellationToken,
        MySqlTransaction? transaction = null)
        where T : struct
    {
        var connection = dbContext.GetDbConnection();
        using var command = connection.CreateCommand();

        CommonHelper.SetupMySqlCommandWithParameters(command, procedureName, parameters, transaction);

        return await ExecuteQueryAsync<T>(connection, command, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<DatabaseQueryScalarResponse<T>> ExecuteQueryAsync<T>(
        DbConnection connection,
        MySqlCommand command,
        CancellationToken cancellationToken)
        where T : struct
    {
        try
        {
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            }

            var result = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

            var errorMessage = command.Parameters[DefaultConstants.ErrorMessageParameter].Value?.ToString();
            _ = int.TryParse(command.Parameters[DefaultConstants.ErrorCodeParameter].Value?.ToString(), NumberStyles.None, provider: null, out var errorCode);

            return new DatabaseQueryScalarResponse<T>
            {
                Success = errorMessage?.Equals(DefaultConstants.SuccessfulMessage, StringComparison.Ordinal) == true,
                ErrorMessage = errorMessage ?? DefaultConstants.NoErrorMessage,
                ErrorCode = errorCode,
                Data = result is null ? default : (T)result,
            };
        }
        catch (Exception ex)
        {
            return DatabaseQueryScalarResponse.Failure<T>(ex.Message);
        }
    }
    #endregion
}
