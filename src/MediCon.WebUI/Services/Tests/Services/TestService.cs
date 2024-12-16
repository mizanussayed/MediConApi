using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Services.Tests.Models;

namespace MediCon.WebUI.Services.Tests.Services;

public class TestService : ITestService
{
    private readonly IHttpClientFactory _httpClientFactory;

    #region URL
    private const string GetProductsURL = "api/Test/get-all-products";
    #endregion

    public TestService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<IList<ProductModel>>> GetProducts(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiNoAuth);

        var response = await client.GetFromJsonAsync<ApiResponse<IList<ProductModel>>>(GetProductsURL, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (response is null)
        {
            return ApiResponse.NoResponse<IList<ProductModel>>();
        }

        return response;
    }
}
