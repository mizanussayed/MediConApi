using MediCon.Core.Configurations.Helpers;
using MediCon.Core.Configurations.UnitOfWorks;
using MediCon.Core.Features.ServiceTypes.Entity;
using MediCon.Core.Features.ServiceTypes.Model;
using MediCon.Core.Features.ServiceTypes.Repository;

using FluentValidation;

using Lib.ErrorOr;

using Mapster;

namespace MediCon.Core.Features.ServiceTypes.Service;

public class ServiceTypeService(IUnitOfWork unitOfWork,
    IValidator<ServiceTypeRequestModel> validator)
{
    public async Task<ErrorOr<IEnumerable<ServiceTypeResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IServiceTypeRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<ServiceTypeResponseModel>>());
    }

    public async Task<ErrorOr<ServiceTypeResponseModel>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IServiceTypeRepository>();

        var dbResult = await repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<ServiceTypeResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(ServiceTypeRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(requestModel, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors.ToError();
        }

        var repository = unitOfWork.Repository<IServiceTypeRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<ServiceType>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(ServiceTypeRequestModel requestModel, long id, long userId, CancellationToken cancellationToken)
    {

        var validationResult = await validator.ValidateAsync(requestModel, cancellationToken).ConfigureAwait(false);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors.ToError();
        }

        var repository = unitOfWork.Repository<IServiceTypeRepository>();


        var dbResult = await repository.UpdateAsync(requestModel.Adapt<ServiceType>(), id, userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IServiceTypeRepository>();

        var dbResult = await repository.DeleteAsync(id, userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
