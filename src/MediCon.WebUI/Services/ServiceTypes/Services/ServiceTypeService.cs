using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.ServiceTypes.Models;

namespace MediCon.WebUI.Services.ServiceTypes.Services;

public class ServiceTypeService : IServiceTypeService
{
    private readonly IHttpClientFactory _httpClientFactory;

    #region URL
    private const string GetURL = "api/ServiceType/Get";
    private const string GetByIdURL = "api/ServiceType/GetById";
    private const string CreateURL = "api/ServiceType/Create";
    private const string UpdateURL = "api/ServiceType/Update";
    private const string DeleteURL = "api/ServiceType/Delete";
    #endregion

    public ServiceTypeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<EmptyApiResponse> Create(ServiceTypeRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.PostAsJsonAsync(CreateURL, model, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetEmptyApiResponseFromJsonAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<EmptyApiResponse> Delete(long id, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.DeleteAsync($"{DeleteURL}/{id.ToString(provider: null)}", cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetEmptyApiResponseFromJsonAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<ServiceTypeResponseModel>>> Get(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.GetAsync(GetURL, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<ServiceTypeResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<ServiceTypeResponseModel>> GetById(long id, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.GetAsync($"{GetByIdURL}/{id.ToString(provider: null)}", cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<ServiceTypeResponseModel>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<EmptyApiResponse> Update(long id, ServiceTypeRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.PutAsJsonAsync($"{UpdateURL}/{id.ToString(provider: null)}", model, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetEmptyApiResponseFromJsonAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
