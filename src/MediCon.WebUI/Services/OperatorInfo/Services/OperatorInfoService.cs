using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.OperatorInfo.Models;

namespace MediCon.WebUI.Services.OperatorInfo.Services
{
    public class OperatorInfoService : IOperatorInfoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        #region URL
        private const string GetURL = "api/Operator/Get";
        private const string GetByIdURL = "api/Operator/GetById";
        private const string CreateURL = "api/Operator/Create";
        private const string UpdateURL = "api/Operator/Update";
        private const string DeleteURL = "api/Operator/Delete";
        #endregion

        public OperatorInfoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<EmptyApiResponse> Create(OperatorInfoRequestModel model, CancellationToken cancellationToken = default)
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

        public async Task<ApiResponse<IList<OperatorInfoResponseModel>>> Get(CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
            var response = await client.GetAsync(GetURL, cancellationToken: cancellationToken).ConfigureAwait(false);
            return await response.GetApiResponseFromJsonAsync<IList<OperatorInfoResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<OperatorInfoResponseModel>> GetById(long id, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
            var response = await client.GetAsync($"{GetByIdURL}/{id.ToString(provider: null)}", cancellationToken: cancellationToken).ConfigureAwait(false);
            return await response.GetApiResponseFromJsonAsync<OperatorInfoResponseModel>(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<EmptyApiResponse> Update(long id, OperatorInfoRequestModel model, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient(HttpClientKey.ApiAuth);
            var response = await client.PutAsJsonAsync($"{UpdateURL}/{id.ToString(provider: null)}", model, cancellationToken: cancellationToken).ConfigureAwait(false);
            return await response.GetEmptyApiResponseFromJsonAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
