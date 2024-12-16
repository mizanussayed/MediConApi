using MediCon.Core.Configurations.Constants;
using MediCon.Core.Configurations.Settings;
using MediCon.Core.Configurations.Settings.ApiUrl;
using MediCon.Core.Features.Users.Model.ApiResponse;

using Lib.ErrorOr;

using System.Net.Http.Json;

namespace MediCon.Core.Features.Users.Service;

public class UserExternalApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISettingsHelper _settingsHelper;

    public UserExternalApiService(IHttpClientFactory httpClientFactory, ISettingsHelper settingsHelper)
    {
        _httpClientFactory = httpClientFactory;
        _settingsHelper = settingsHelper;
    }

    public async Task<ErrorOr<UserLoginAPIResponse<UserLoginApiResponseModel>>> Login(string userName, string password, CancellationToken cancellationToken)
    {
        const string url = "account/login";
        var client = _httpClientFactory.CreateClient(HttpClientKey.LoginApi);
        var loginApiSettings = _settingsHelper.Get<LoginApiSettings>(LoginApiSettings.SectionName);

        var response = await client.PostAsJsonAsync(new Uri(client.BaseAddress + url, UriKind.Absolute), new
        {
            loginApiSettings.ApplicationName,
            loginApiSettings.ApplicationKey,
            userName,
            password,
            RememberMe = true,
        }, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (response is null)
        {
            return Error.Unexpected("Unable to login");
        }
        var userResponse = await response.Content.ReadFromJsonAsync<UserLoginAPIResponse<UserLoginApiResponseModel>>(cancellationToken: cancellationToken).ConfigureAwait(false);
        if (userResponse is null)
        {
            return Error.Unexpected("Unable to login");
        }
        if (!response.IsSuccessStatusCode || !userResponse.IsSuccess)
        {
            return Error.Failure(userResponse.Message);
        }
        if (userResponse.Data is null)
        {
            return Error.Unexpected("Unable to login");
        }

        return userResponse;
    }
}
