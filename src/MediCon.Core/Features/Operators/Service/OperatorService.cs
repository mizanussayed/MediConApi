using MediCon.Core.Configurations.Helpers;
using MediCon.Core.Configurations.UnitOfWorks;
using MediCon.Core.Features.Operators.Entity;
using MediCon.Core.Features.Operators.Model;
using MediCon.Core.Features.Operators.Repository;

using FluentValidation;

using Lib.ErrorOr;

using Mapster;

namespace MediCon.Core.Features.Operators.Service;

public class OperatorService(IUnitOfWork unitOfWork,
    IValidator<OperatorRequestModel> validator)
{
    public async Task<ErrorOr<IEnumerable<OperatorResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IOperatorRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<OperatorResponseModel>>());
    }

    public async Task<ErrorOr<OperatorResponseModel>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IOperatorRepository>();

        var dbResult = await repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<OperatorResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(OperatorRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {

        var validationResult = await validator.ValidateAsync(requestModel, cancellationToken).ConfigureAwait(false);

        if (!validationResult.IsValid)
        {
            return validationResult.Errors.ToError();
        }

        var repository = unitOfWork.Repository<IOperatorRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Operator>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(OperatorRequestModel requestModel, long id, long userId, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(requestModel, cancellationToken: cancellationToken).ConfigureAwait(false);

        var repository = unitOfWork.Repository<IOperatorRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Operator>(), id, userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IOperatorRepository>();

        var dbResult = await repository.DeleteAsync(id, userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
