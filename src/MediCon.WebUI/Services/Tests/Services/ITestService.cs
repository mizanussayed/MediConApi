using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.Tests.Models;

namespace MediCon.WebUI.Services.Tests.Services;

public interface ITestService
{
    Task<ApiResponse<IList<ProductModel>>> GetProducts(CancellationToken cancellationToken = default);
}
