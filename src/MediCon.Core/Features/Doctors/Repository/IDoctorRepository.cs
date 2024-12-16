using Lib.ErrorOr;

using MediCon.Core.Features.Doctors.Entity;

namespace MediCon.Core.Features.Doctors.Repository;

public interface IDoctorRepository
{
    Task<ErrorOr<IEnumerable<Doctor>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ErrorOr<Doctor>> GetAsync(long id, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> CreateAsync(Doctor entity, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> UpdateAsync(Doctor entity, long userId, CancellationToken cancellationToken);
    Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken);
}