using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Hospitals.Entity;
using MediCon.Core.Features.Hospitals.Model;
using MediCon.Core.Features.Hospitals.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Hospitals.Service;

public class HospitalService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<HospitalResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IHospitalRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<HospitalResponseModel>>());
    }

    public async Task<ErrorOr<HospitalResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IHospitalRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<HospitalResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(HospitalRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IHospitalRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Hospital>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(HospitalRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IHospitalRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Hospital>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IHospitalRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
