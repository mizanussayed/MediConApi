using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.RefreshTokens.Models;

namespace MediCon.WebUI.Services.RefreshTokens.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IHttpClientFactory _httpClientFactory;

    #region URL
    public const string GetNewRefreshTokenURL = "api/RefreshToken/GetNewRefreshToken";
    #endregion

    public RefreshTokenService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<NewTokenResponseModel>> GetNewToken(NewTokenRequestModel model, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientKey.ApiNoAuth);
        var response = await httpClient.PostAsJsonAsync(GetNewRefreshTokenURL, model, cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<NewTokenResponseModel>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
