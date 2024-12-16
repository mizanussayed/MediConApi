using Lib.ErrorOr;

using MediCon.Core.Features.Facilities.Entity;
using MediCon.Core.Features.Facilities.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Facilities;

internal sealed class FacilityRepository(MediconcernDbContext dbContext) : IFacilityRepository
{
    public async Task<ErrorOr<IEnumerable<Facility>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Facilities.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all facilities.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Facility>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Facilities.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get facility.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Facility entity, CancellationToken cancellationToken)
    {
        dbContext.Facilities.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create facility.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        dbContext.Facilities.Remove(new Facility { Id = id });

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete facility.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Facility entity, long userId, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Facilities.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);
        if (dbEntity == null)
        {
            return Error.Unexpected("Facility not found.");
        }

        dbEntity.Name = entity.Name;
        dbEntity.Details = entity.Details;
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update facility.");
        }
        return SuccessType.Success;
    }
}
