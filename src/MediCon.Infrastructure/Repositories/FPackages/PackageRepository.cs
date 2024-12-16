
using Lib.ErrorOr;

using MediCon.Core.Features.FPackages.Entity;
using MediCon.Core.Features.FPackages.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.FPackages;

internal sealed class PackageRepository(MediconcernDbContext dbContext) : IPackageRepository
{
    public async Task<ErrorOr<IEnumerable<Package>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Packages.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all packages.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Package>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Packages.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get doctor.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Package entity, CancellationToken cancellationToken)
    {
        dbContext.Packages.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create doctor.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        dbContext.Packages.Remove(new Package { Id = id });

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to delete package.");
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(Package entity, long userId, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Packages.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);
        if (dbEntity == null)
        {
            return Error.Unexpected("City not found.");
        }

        dbEntity.Name = entity.Name;
        dbEntity.ShortDescription = entity.ShortDescription;
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update package.");
        }
        return SuccessType.Success;
    }
}
