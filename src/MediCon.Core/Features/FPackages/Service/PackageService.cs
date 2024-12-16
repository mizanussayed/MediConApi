using Lib.ErrorOr;

using Mapster;

using MediCon.Core.Features.FPackages.Entity;
using MediCon.Core.Features.FPackages.Model;
using MediCon.Core.Features.FPackages.Repository;
using MediCon.Core.Configurations.UnitOfWorks;

namespace MediCon.Core.Features.FPackages.Service;

public class PackageService(IUnitOfWork unitOfWork)
{
    public async Task<ErrorOr<IEnumerable<PackageResponseModel>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IPackageRepository>();
        var dbResult = await repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<IEnumerable<PackageResponseModel>>());
    }

    public async Task<ErrorOr<PackageResponseModel>> GetAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IPackageRepository>();

        var dbResult = await repository.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return ErrorOr.From(dbResult.Value.Adapt<PackageResponseModel>());
    }

    public async Task<ErrorOr<Success>> CreateAsync(PackageRequestModel requestModel, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IPackageRepository>();

        var dbResult = await repository.CreateAsync(requestModel.Adapt<Package>(), cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> UpdateAsync(PackageRequestModel requestModel, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IPackageRepository>();

        var dbResult = await repository.UpdateAsync(requestModel.Adapt<Package>(), userId, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }

    public async Task<ErrorOr<Success>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IPackageRepository>();

        var dbResult = await repository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

        if (dbResult.IsError)
        {
            return dbResult.Error;
        }
        return SuccessType.Success;
    }
}
