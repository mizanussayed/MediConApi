using MediCon.Core.Features.Operators.Entity;
using MediCon.Core.Features.Operators.Repository;
using MediCon.Infrastructure.Configurations.DbContexts;

using Lib.DBAccess.Builder;
using Lib.DBAccess.NonQueries;
using Lib.DBAccess.Queries;
using Lib.ErrorOr;


namespace MediCon.Infrastructure.Repository.Operators;

internal sealed class OperatorRepository(OracleDMSPhaseFourDbContext dbContext) : IOperatorRepository
{
    public async Task<ErrorOr<IEnumerable<Operator>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
                         .Oracle()
                         .BuildForQuery();
        var dbResult = await dbContext.QueryListAsync<Operator>(OperatorProcedureNames.GET_CMP_OPERATORINFO, parameters, Operator.MapFromDbWithReader, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Unexpected(dbResult.ErrorMessage ?? "Failed to get all operator.");
        }
        return dbResult.Data;
    }

    public async Task<ErrorOr<Operator?>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
                         .Oracle()
                         .AddLongParameter("P_ID", id)
                         .BuildForQuery();
        var dbResult = await dbContext.QueryFirstAsync<Operator>(OperatorProcedureNames.GET_CMP_OPERATORINFO_BY_ID, parameters, Operator.MapFromDbWithReader, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Unexpected(dbResult.ErrorMessage ?? "Failed to get operator.");
        }
        return dbResult.Data;
    }

    public async Task<ErrorOr<bool>> CreateAsync(Operator entity, long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
                         .Oracle()
                         .AddVarchar2Parameter("P_NAME", entity.Name)
                         .AddVarchar2Parameter("P_BINNUMBER", entity.BinNumber)
                         .AddVarchar2Parameter("P_TINNUMBER", entity.TinNumber)
                         .AddVarchar2Parameter("P_ADDRESS", entity.Address)
                         .AddVarchar2Parameter("P_POCNAME", entity.POCNAME)
                         .AddVarchar2Parameter("P_POCDESIGNATION", entity.POCDESIGNATION)
                         .AddLongParameter("P_USERID", userId)
                         .Build();

        var dbResult = await dbContext.ExecuteNonQueryAsync(OperatorProcedureNames.INSERT_CMP_OPERATORINFO, parameters, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Unexpected(dbResult.ErrorMessage ?? "Failed to create operator.");
        }
        return dbResult.Success;
    }

    public async Task<ErrorOr<bool>> DeleteAsync(long id, long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
                         .Oracle()
                         .AddLongParameter("P_ID", id)
                         .AddLongParameter("P_USERID", userId)
                         .Build();
        var dbResult = await dbContext.ExecuteNonQueryAsync(OperatorProcedureNames.DELETE_CMP_OPERATORINFO, parameters, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Unexpected(dbResult.ErrorMessage ?? "Failed to delete operator.");
        }
        return dbResult.Success;
    }

    public async Task<ErrorOr<bool>> UpdateAsync(Operator entity, long id, long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
                  .Oracle()
                  .AddLongParameter("P_ID", id)
                  .AddVarchar2Parameter("P_NAME", entity.Name)
                  .AddVarchar2Parameter("P_BINNUMBER", entity.BinNumber)
                  .AddVarchar2Parameter("P_TINNUMBER", entity.TinNumber)
                  .AddVarchar2Parameter("P_ADDRESS", entity.Address)
                  .AddVarchar2Parameter("P_POCNAME", entity.POCNAME)
                  .AddVarchar2Parameter("P_POCDESIGNATION", entity.POCDESIGNATION)
                  .AddLongParameter("P_USERID", userId)
                  .Build();

        var dbResult = await dbContext.ExecuteNonQueryAsync(OperatorProcedureNames.UPDATE_CMP_OPERATORINFO, parameters, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Unexpected(dbResult.ErrorMessage ?? "Failed to create operator.");
        }
        return dbResult.Success;
    }
}
