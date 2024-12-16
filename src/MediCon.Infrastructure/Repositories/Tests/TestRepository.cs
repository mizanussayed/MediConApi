using MediCon.Core.Features.Tests.Entity;
using MediCon.Core.Features.Tests.Repository;
using MediCon.Infrastructure.Configurations.DbContexts;

using Lib.DBAccess.Builder;
using Lib.DBAccess.Contexts;
using Lib.DBAccess.NonQueries;
using Lib.DBAccess.Queries;
using Lib.ErrorOr;

using Serilog;

namespace MediCon.Infrastructure.Repositories.Tests;

public class TestRepository : ITestRepository
{
    private readonly OracleCFDBDbContext _oracleDbContext;
    private readonly MySqlDbContext _mySqlDbContext;

    public TestRepository(OracleCFDBDbContext oracleDbContext, MySqlDbContext mySqlDbContext)
    {
        _oracleDbContext = oracleDbContext;
        _mySqlDbContext = mySqlDbContext;
    }

    public async Task<ErrorOr<bool>> CreateMarketVisit(CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .MySQL()
            .AddVarcharParameter("P_SURVEY_NAME", "ABC")
            .AddVarcharParameter("P_ZM_CODE", "RS0149")
            .AddDateTimeParameter("P_START_DATE", value: null)
            .AddDateTimeParameter("P_END_DATE", value: null)
            .AddIntParameter("P_USER_ID", 12104)
            .AddIntParameter("P_IS_ACTIVE", 1)
            .Build();

        using var transaction = await _mySqlDbContext.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var dbResult = await _mySqlDbContext.ExecuteNonQueryAsync(
                "SP_INS_ZM_MARKET_VISIT_TEMP",
                parameters,
                cancellationToken,
                transaction).ConfigureAwait(false);

            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

            if (!dbResult.Success)
            {
                return Error.Failure(dbResult.ErrorMessage);
            }

            return dbResult.Success;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

            return Error.Unexpected("Failed to create market visit. Reason: Database");
        }
    }

    public async Task<ErrorOr<IEnumerable<ProductTest>>> GetAllProductTestAsync(CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .BuildForQuery();

        var dbResult = await _oracleDbContext.QueryListAsync<ProductTest>(
            "GET_PRODUCT",
            parameters,
            ProductTest.MapFromDbWithDataRow,
            cancellationToken).ConfigureAwait(false);

        Log.Information("Name is Nasim");

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }

        return dbResult.Data;
    }

    public async Task<ErrorOr<IEnumerable<Vendor>>> GetVendors(CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .MySQL()
            .AddIntParameter("p_BASE_MONTH_ID", 130)
            .AddIntParameter("p_USERID", 14042)
            .AddIntParameter("p_DDID", 0)
            .Build();

        var dbResult = await _mySqlDbContext.QueryListAsync<Vendor>(
            "SP_GET_RSO_COMMISSION_SUMMSRY",
            parameters,
            Vendor.MapFromDbWithDataRow,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }

        return dbResult.Data;
    }

    public async Task<ErrorOr<ApplicationUser>> GetApplicationUserAsync(CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddVarchar2Parameter("P_Name", "POS")
            .AddVarchar2Parameter("P_Key", "duEy4Zep6P7T4svLLT7C39fuCKigmH")
            .BuildForQuery();

        var dbResult = await _oracleDbContext.QueryFirstAsync<ApplicationUser>(
            "USER_AUTHENTICATION.VALIDATEAPPLICATIONUSER",
            parameters,
            ApplicationUser.MapFromDbWithReader,
            cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage);
        }

        return dbResult.Data;
    }
}
