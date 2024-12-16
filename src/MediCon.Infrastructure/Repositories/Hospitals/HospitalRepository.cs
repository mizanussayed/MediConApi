using Lib.ErrorOr;

using MediCon.Core.Features.Hospitals.Entity;
using MediCon.Core.Features.Hospitals.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Hospitals;

internal sealed class HospitalRepository(MediconcernDbContext dbContext) : IHospitalRepository
{
    public async Task<ErrorOr<IEnumerable<Hospital>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Hospitals.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all hospital.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Hospital>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Hospitals.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get hospital.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Hospital entity, CancellationToken cancellationToken)
    {
        dbContext.Hospitals.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create hospital.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        dbContext.Hospitals.Remove(new Hospital { Id = id });

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete hospital.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Hospital entity, long userId, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Hospitals.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);
        if (dbEntity == null)
        {
            return Error.Unexpected("Hospital not found.");
        }

        dbEntity.Name = entity.Name;
        dbEntity.Location = entity.Location;

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update hospital.");
        }
        return SuccessType.Success;
    }
}
