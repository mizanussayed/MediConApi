
using MediCon.Core.Features.Operators.Entity;
using MediCon.Core.Features.Operators.Repository;
using MediCon.Infrastructure.Configurations.DbContexts;

using Lib.ErrorOr;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Operators;

internal sealed class OperatorRepositorycopy(MediconcernDbContext dbContext) : IOperatorRepository
{
    public async Task<ErrorOr<bool>> CreateAsync(Operator entity, long userId, CancellationToken cancellationToken)
    {
        dbContext.Operators.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create operator.");
        }
        return true;
    }

    public async Task<ErrorOr<bool>> DeleteAsync(long id, long userId, CancellationToken cancellationToken)
    {
        dbContext.Operators.Remove(new Operator { Id = id });

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete operator.");
        }
        return true;
    }

    public async Task<ErrorOr<IEnumerable<Operator>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Operators.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all operator.");
        }
        return dbResult;

    }

    public async Task<ErrorOr<Operator?>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
       var dbResult = await dbContext.Operators.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get operator.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<bool>> UpdateAsync(Operator entity, long id, long userId, CancellationToken cancellationToken)
    {
        var operatorEntity = await dbContext.Operators.FindAsync(id, cancellationToken).ConfigureAwait(false);
        if (operatorEntity == null)
        {
            return Error.Unexpected("Operator not found.");
        }

        operatorEntity.Name = entity.Name;
        operatorEntity.BinNumber = entity.BinNumber;
        operatorEntity.TinNumber = entity.TinNumber;
        operatorEntity.Address = entity.Address;
        operatorEntity.POCNAME = entity.POCNAME;
        operatorEntity.POCDESIGNATION = entity.POCDESIGNATION;
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update operator.");
        }
        return true;
    }
}
