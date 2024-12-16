
using Lib.ErrorOr;

using MediCon.Core.Features.Specialities.Entity;
using MediCon.Core.Features.Specialities.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Specialities;

internal sealed class SpecialityRepository(MediconcernDbContext dbContext) : ISpecialityRepository
{
    public async Task<ErrorOr<IEnumerable<Speciality>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Specialities.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all speciality.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Speciality>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Specialities.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get speciality.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Speciality entity, CancellationToken cancellationToken)
    {
        dbContext.Specialities.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create spaciality.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
       dbContext.Remove(new Speciality { Id = id });
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete speciality.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Speciality entity, CancellationToken cancellationToken)
    {
       var dbResult = await dbContext.Specialities.FindAsync(entity.Id).ConfigureAwait(false);
        if (dbResult == null)
        {
            return Error.Unexpected("Failed to update speciality.");
        }
        dbResult.Name = entity.Name;
        dbResult.ShortDescription = entity.ShortDescription;
        dbResult.Description = entity.Description;
        dbResult.IconUrl = entity.IconUrl;
        dbResult.ImageUrl = entity.ImageUrl;
        var result = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (result == 0)
        {
            return Error.Unexpected("Failed to update speciality.");
        }
        return SuccessType.Success;
    }
}
