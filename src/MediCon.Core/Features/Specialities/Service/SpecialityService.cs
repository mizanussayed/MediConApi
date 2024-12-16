using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Specialities.Entity;
using MediCon.Core.Features.Specialities.Model;
using MediCon.Core.Features.Specialities.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Specialities.Service;

public class SpecialityService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<SpecialityResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ISpecialityRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<SpecialityResponseModel>>());
    }

    public async Task<ErrorOr<SpecialityResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ISpecialityRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<SpecialityResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(SpecialityRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ISpecialityRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Speciality>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(SpecialityRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ISpecialityRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Speciality>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ISpecialityRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
