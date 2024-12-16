
using Lib.ErrorOr;

using MediCon.Core.Features.Countries.Entity;
using MediCon.Core.Features.Countries.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Countries;

internal sealed class CountryRepository(MediconcernDbContext dbContext) : ICountryRepository
{
    public async Task<ErrorOr<IEnumerable<Country>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Countries.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult is null)
        {
            return Error.Unexpected("Failed to get countries.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Country>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);

        if (dbResult is null)
        {
            return Error.NotFound("Country not found.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Country entity, CancellationToken cancellationToken)
    {
        dbContext.Countries.Add(entity);
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create country.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
        if (entity is null)
        {
            return Error.NotFound("Country not found.");
        }
        dbContext.Countries.Remove(entity);
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete country.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Country entity, long userId, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Countries.FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken).ConfigureAwait(false);
        if (dbEntity is null)
        {
            return Error.NotFound("Country not found.");
        }
        dbEntity.Name = entity.Name;

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update country.");
        }
        return SuccessType.Success;
    }
}
