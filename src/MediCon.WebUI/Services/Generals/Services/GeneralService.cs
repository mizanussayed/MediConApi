using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.Generals.Models;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MediCon.WebUI.Services.Generals.Services;

public class GeneralService : IGeneralService
{
    private readonly IHttpClientFactory _httpClientFactory;

    #region URL
    public const string GetInvoiceDurationURL = "api/General/GetInvoiceDuration";
    private const string GetBankDropdownURL = "api/General/GetBank";
    private const string GetDisburseBankDropdownURL = "api/General/GetDisburseBank";
    
    private const string GetCenterURL = "api/General/GetCenter";
    private const string GetRegionURL = "api/General/GetRegion";
    private const string GetCollectionTypeURL = "api/General/GetCollectionType";
    private const string GetSubCreditAccountURL = "api/General/GetSubCreditAccount";
    
    private const string GetCollectionModeURL = "api/General/GetCollectionMode";
    private const string GetDistributorByIdURL = "api/General/GetDistributorById";
    private const string GetConfigurationURL = "api/General/GetConfigurations";
    #endregion

    public GeneralService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<IList<InvoiceDurationResponseModel>>> GetInvoiceDuration(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.GetAsync(GetInvoiceDurationURL, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<InvoiceDurationResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<ConfigurationResponseModel>> GetConfiguration(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.GetAsync(GetConfigurationURL, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<ConfigurationResponseModel>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
    public async Task<ApiResponse<IList<BankResponseModel>>> GetBankDropdown(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.GetAsync(GetBankDropdownURL, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<BankResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<BankResponseModel>>> GetDisburseBankDropdown(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var response = await client.GetAsync(GetDisburseBankDropdownURL, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<BankResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<CenterResponseModel>>> GetCenter(CenterRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.UserId is not null)
        {
            parameters.Add(new("UserId", model.UserId?.ToString(provider: null) ?? string.Empty));
        }

        var queryBuilder = new QueryBuilder(parameters);
        var response = await client.GetAsync(GetCenterURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<CenterResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<RegionResponseModel>>> GetRegion(RegionRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.CenterId is not null)
        {
            parameters.Add(new("CenterId", model.CenterId?.ToString(provider: null) ?? string.Empty));
        }
        if (model.DistributorId is not null)
        {
            parameters.Add(new("DistributorId", model.DistributorId?.ToString(provider: null) ?? string.Empty));
        }

        var queryBuilder = new QueryBuilder(parameters);
        var response = await client.GetAsync(GetRegionURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<RegionResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<CollectionTypeResponseModel>>> GetCollectionType(CollectionTypeRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.CollectionTypeId is not null && model.IsRFCollection is not null)
        {
            parameters.Add(new("CollectionTypeId", model.CollectionTypeId?.ToString(provider: null) ?? string.Empty));
            parameters.Add(new("IsRFCollection", model.IsRFCollection?.ToString(provider: null) ?? string.Empty));
        }

        var queryBuilder = new QueryBuilder(parameters);
        var response = await client.GetAsync(GetCollectionTypeURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<CollectionTypeResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    

        public async Task<ApiResponse<IList<SubCreditAccountResponseModel>>> GetSubCreditAccount(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var parameters = new List<KeyValuePair<string, string>> { };
     

        var queryBuilder = new QueryBuilder(parameters);
        var response = await client.GetAsync(GetSubCreditAccountURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<SubCreditAccountResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<IList<CollectionModeResponseModel>>> GetCollectionMode(CollectionModeRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.CollectionModeId is not null)
        {
            parameters.Add(new("CollectionModeId", model.CollectionModeId?.ToString(provider: null) ?? string.Empty));
        }

        var queryBuilder = new QueryBuilder(parameters);
        var response = await client.GetAsync(GetCollectionModeURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<IList<CollectionModeResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<ApiResponse<DistributorResponseModel>> GetDistributorById(DistributorRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
        var parameters = new List<KeyValuePair<string, string>> { };
        if (model.Id is not null)
        {
            parameters.Add(new("Id", model.Id?.ToString(provider: null) ?? string.Empty));
        }

        var queryBuilder = new QueryBuilder(parameters);
        var response = await client.GetAsync(GetDistributorByIdURL + queryBuilder, cancellationToken: cancellationToken).ConfigureAwait(false);
        var result = await response.GetApiResponseFromJsonAsync<DistributorResponseModel>(cancellationToken: cancellationToken).ConfigureAwait(false);

        return result;
    }
}
