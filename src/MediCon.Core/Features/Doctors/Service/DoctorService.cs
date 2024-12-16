using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Doctors.Entity;
using MediCon.Core.Features.Doctors.Model;
using MediCon.Core.Features.Doctors.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Doctors.Service;

public class DoctorService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<DoctorResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IDoctorRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<DoctorResponseModel>>());
    }

    public async Task<ErrorOr<DoctorResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IDoctorRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<DoctorResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(DoctorRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IDoctorRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Doctor>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(DoctorRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IDoctorRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Doctor>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IDoctorRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
