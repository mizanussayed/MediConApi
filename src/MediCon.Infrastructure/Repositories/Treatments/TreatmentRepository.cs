
using Lib.ErrorOr;

using MediCon.Core.Features.Treatments.Entity;
using MediCon.Core.Features.Treatments.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Treatments;

internal sealed class TreatmentRepository(MediconcernDbContext dbContext) : ITreatmentRepository
{
    public async Task<ErrorOr<IEnumerable<Treatment>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Treatments.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all treatement.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Treatment>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Treatments.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get treatement.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Treatment entity, CancellationToken cancellationToken)
    {
        dbContext.Treatments.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create treatment.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        dbContext.Remove(new Treatment { Id = id });

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete treatment.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Treatment entity, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Treatments.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);

        if (dbEntity == null)
        {
            return Error.Unexpected("Failed to update treatment.");
        }

        dbEntity.Name = entity.Name;
        dbEntity.ShortDescription = entity.ShortDescription;
        dbEntity.Description = entity.Description;
        dbEntity.IconUrl = entity.IconUrl;
        dbEntity.ImageUrl = entity.ImageUrl;

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update treatment.");
        }
        return SuccessType.Success;
    }
}
