using Lib.ErrorOr;

using MediCon.Core.Features.Cities.Entity;
using MediCon.Core.Features.Doctors.Entity;
using MediCon.Core.Features.Doctors.Repository;

using MediCon.Infrastructure.Configurations.DbContexts;

using Microsoft.EntityFrameworkCore;


namespace MediCon.Infrastructure.Repositories.Doctors;

internal sealed class DoctorRepository(MediconcernDbContext dbContext) : IDoctorRepository
{
    public async Task<ErrorOr<IEnumerable<Doctor>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Doctors.ToListAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get all doctors.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Doctor>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var dbResult = await dbContext.Doctors.FindAsync(id).ConfigureAwait(false);

        if (dbResult == null)
        {
            return Error.Unexpected("Failed to get doctor.");
        }
        return dbResult;
    }

    public async Task<ErrorOr<Success>> CreateAsync(Doctor entity, CancellationToken cancellationToken)
    {
        dbContext.Doctors.Add(entity);

        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to create doctor.");
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

    public async Task<ErrorOr<Success>> UpdateAsync(Doctor entity, long userId, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext.Doctors.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);
        if (dbEntity == null)
        {
            return Error.Unexpected("City not found.");
        }

        dbEntity.Name = entity.Name;
        dbEntity.ShortDescription = entity.ShortDescription;
        var dbResult = await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult == 0)
        {
            return Error.Unexpected("Failed to update city.");
        }
        return SuccessType.Success;
    }
}
