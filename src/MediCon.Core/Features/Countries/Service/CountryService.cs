using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Countries.Entity;
using MediCon.Core.Features.Countries.Model;
using MediCon.Core.Features.Countries.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Countries.Service;

public class CountryService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<CountryResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICountryRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<CountryResponseModel>>());
    }

    public async Task<ErrorOr<CountryResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICountryRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<CountryResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(CountryRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICountryRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Country>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(CountryRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICountryRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Country>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ICountryRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
