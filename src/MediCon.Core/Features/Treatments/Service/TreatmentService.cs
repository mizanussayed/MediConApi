using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.Treatments.Entity;
using MediCon.Core.Features.Treatments.Model;
using MediCon.Core.Features.Treatments.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.Treatments.Service;

public class TreatmentService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<TreatmentResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ITreatmentRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<TreatmentResponseModel>>());
    }

    public async Task<ErrorOr<TreatmentResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ITreatmentRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<TreatmentResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(TreatmentRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ITreatmentRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Treatment>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(TreatmentRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ITreatmentRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Treatment>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ITreatmentRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
