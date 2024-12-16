using Lib.ErrorOr;

using MediCon.Core.Features.Cities.Entity;
using MediCon.Core.Features.Cities.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Cities;

internal sealed class CityRepository(MediconcernDbContext dbContext) : ICityRepository
{
    public async Task<ErrorOr<IEnumerable<City>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Cities.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all cites.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<City>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Cities.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get city.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(City entity, CancellationToken cancellationToken)
    {
        dbContext.Cities.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create city.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        dbContext.Cities.Remove(new City { Id = id });

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete city.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(City entity, long userId, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Cities.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);
        if (dbEntity == null)
        {
            return Error.Unexpected("City not found.");
        }

        dbEntity.Name = entity.Name;
        dbEntity.CountryId = entity.CountryId;
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update city.");
        }
        return SuccessType.Success;
    }

}
