using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Facilities.Entity;
using MediCon.Core.Features.Facilities.Model;
using MediCon.Core.Features.Facilities.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Facilities.Service;

public class FacilityService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<FacilityResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IFacilityRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<FacilityResponseModel>>());
    }

    public async Task<ErrorOr<FacilityResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IFacilityRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<FacilityResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(FacilityRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IFacilityRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Facility>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(FacilityRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IFacilityRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Facility>(), userId,  cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IFacilityRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
