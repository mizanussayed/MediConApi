using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.Products.Models;

using Microsoft.AspNetCore.Http.Extensions;

namespace MediCon.WebUI.Services.Products.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    #region URL
    public const string GetProductURL = "api/Product/GetProduct";
    public const string GetProductCategoryURL = "api/Product/GetProductCategory";
    #endregion

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<IList<ProductResponseModel>>> GetProduct(ProductRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);

        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.ProductId is not null)
        {
            parameters.Add(new("ProductId", model.ProductId?.ToString(provider: null) ?? string.Empty));
        }
        if (model.CategoryId is not null)
        {
            parameters.Add(new("CategoryId", model.CategoryId?.ToString(provider: null) ?? string.Empty));
        }
        if (model.SubCategoryId is not null)
        {
            parameters.Add(new("SubCategoryId", model.SubCategoryId?.ToString(provider: null) ?? string.Empty));
        }
        if (model.ProductName is not null)
        {
            parameters.Add(new("ProductName", model.ProductName));
        }
        //parameters.Add(new("PageSize", model.PageSize.ToString(provider: null)));
        //parameters.Add(new("PageNo", model.PageNo.ToString(provider: null)));

        var queryBuilder = new QueryBuilder(parameters);

        var response = await client.GetAsync(GetProductURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<ProductResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<ProductCategoryResponseModel>>> GetProductCategory(ProductCategoryRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);

        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.CategoryId is not null)
        {
            parameters.Add(new("CategoryId", model.CategoryId?.ToString(provider: null) ?? string.Empty));
        }
        //parameters.Add(new("PageSize", model.PageSize.ToString(provider: null)));
        //parameters.Add(new("PageNo", model.PageNo.ToString(provider: null)));

        var queryBuilder = new QueryBuilder(parameters);

        var response = await client.GetAsync(GetProductCategoryURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<ProductCategoryResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
