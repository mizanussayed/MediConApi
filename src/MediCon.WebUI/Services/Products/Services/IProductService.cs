using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.Products.Models;

namespace MediCon.WebUI.Services.Products.Services
{
    public interface IProductService
    {
        Task<ApiResponse<IList<ProductResponseModel>>> GetProduct(ProductRequestModel model, CancellationToken cancellationToken = default);
        Task<ApiResponse<IList<ProductCategoryResponseModel>>> GetProductCategory(ProductCategoryRequestModel model, CancellationToken cancellationToken = default);
    }
}
