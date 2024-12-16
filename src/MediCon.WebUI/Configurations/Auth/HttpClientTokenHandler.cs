using System.Net.Http.Headers;

using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.RefreshTokens.Models;
using MediCon.WebUI.Services.RefreshTokens.Services;

namespace MediCon.WebUI.Configurations.Auth;

public class HttpClientTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IDateTimeHelper _dateTimeHelper;

    public HttpClientTokenHandler(IHttpContextAccessor httpContextAccessor, IRefreshTokenService refreshTokenService, IDateTimeHelper dateTimeHelper)
    {
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenService = refreshTokenService;
        _dateTimeHelper = dateTimeHelper;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await SetToken(request, cancellationToken).ConfigureAwait(false);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private async Task SetToken(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return;
        }

        var value = httpContext.GetCurrentUserInfo();
        if (!value.Success)
        {
            return;
        }

        if (_dateTimeHelper.Now > value.CurrentUserInfo.ExpireTime)
        {
            var refreshTokenResult = await _refreshTokenService.GetNewToken(new NewTokenRequestModel { RefreshToken = value.CurrentUserInfo.RefreshToken }, cancellationToken).ConfigureAwait(false);
            if (refreshTokenResult.Success)
            {
                value.CurrentUserInfo.AccessToken = refreshTokenResult.Data?.AccessToken ?? string.Empty;
            }
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", value.CurrentUserInfo.AccessToken);
    }
}
