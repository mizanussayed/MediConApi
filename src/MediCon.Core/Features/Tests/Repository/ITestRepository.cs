using MediCon.Core.Features.Tests.Entity;

using Lib.ErrorOr;

namespace MediCon.Core.Features.Tests.Repository;

public interface ITestRepository
{
    Task<ErrorOr<bool>> CreateMarketVisit(CancellationToken cancellationToken);
    Task<ErrorOr<IEnumerable<ProductTest>>> GetAllProductTestAsync(CancellationToken cancellationToken);
    Task<ErrorOr<IEnumerable<Vendor>>> GetVendors(CancellationToken cancellationToken);
    Task<ErrorOr<ApplicationUser>> GetApplicationUserAsync(CancellationToken cancellationToken);
}
