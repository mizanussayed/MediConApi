using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Cities.Entity;
using MediCon.Core.Features.Cities.Model;
using MediCon.Core.Features.Cities.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Cities.Service;

public class CityService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<CityResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICityRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<CityResponseModel>>());
    }

    public async Task<ErrorOr<CityResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICityRepository>();

        var dbResult = await repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<CityResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(CityRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICityRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<City>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(CityRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICityRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<City>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICityRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
