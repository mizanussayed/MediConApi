using MediCon.Core.Features.ServiceTypes.Entity;
using MediCon.Core.Features.ServiceTypes.Repository;
using MediCon.Infrastructure.Configurations.DbContexts;

using Lib.DBAccess.Builder;
using Lib.DBAccess.NonQueries;
using Lib.DBAccess.Queries;
using Lib.ErrorOr;

namespace MediCon.Infrastructure.Repositories.ServiceTypes;

public class ServiceTypeRepository(OracleDMSPhaseFourDbContext dbContext) : IServiceTypeRepository
{
    public async Task<ErrorOr<IEnumerable<ServiceType>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .BuildForQuery();

        var dbResult = await dbContext.QueryListAsync<ServiceType>(ServiceTypeProcedureNames.GET_CMP_SERVICETYPE, parameters, ServiceType.MapFromDbWithReader, cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage ?? string.Empty);
        }

        return dbResult.Data;
    }
    public async Task<ErrorOr<ServiceType?>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddLongParameter("P_ID", id)
            .BuildForQuery();

        var dbResult = await dbContext.QueryFirstAsync<ServiceType>(ServiceTypeProcedureNames.GET_CMP_SERVICETYPE_BY_ID, parameters, ServiceType.MapFromDbWithReader, cancellationToken).ConfigureAwait(false);

        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage ?? "Fail to get service type");
        }

        return dbResult.Data;
    }

    public async Task<ErrorOr<bool>> CreateAsync(ServiceType entity, long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
            .Oracle()
            .AddVarchar2Parameter("P_NAME", entity.Name)
            .AddLongParameter("P_USERID", userId)
            .Build();

        var dbResult = await dbContext.ExecuteNonQueryAsync(ServiceTypeProcedureNames.INSERT_CMP_SERVICETYPE, parameters, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage ?? "Fail to insert service type");
        }
        return dbResult.Success;
    }

    public async Task<ErrorOr<bool>> UpdateAsync(ServiceType entity, long id, long userId, CancellationToken cancellationToken)
    {
        var parameters = ParameterBuilder
          .Oracle()
          .AddLongParameter("P_ID", id)
          .AddVarchar2Parameter("P_NAME", entity.Name)
          .AddLongParameter("P_USERID", userId)
          .Build();

        var dbResult = await dbContext.ExecuteNonQueryAsync(ServiceTypeProcedureNames.UPDATE_CMP_SERVICETYPE, parameters, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage ?? "Fail to update service type");
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

        var dbResult = await dbContext.ExecuteNonQueryAsync(ServiceTypeProcedureNames.DELETE_CMP_SERVICETYPE, parameters, cancellationToken).ConfigureAwait(false);
        if (!dbResult.Success)
        {
            return Error.Failure(dbResult.ErrorMessage ?? "Fail to delete service type");
        }
        return dbResult.Success;
    }
}
