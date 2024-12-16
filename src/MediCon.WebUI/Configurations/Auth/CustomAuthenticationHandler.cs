using System.Security.Claims;
using System.Text.Encodings.Web;

using MediCon.WebUI.Configurations.Constants;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace MediCon.WebUI.Configurations.Auth;

public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationScheme>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthenticationHandler(
        IOptionsMonitor<CustomAuthenticationScheme> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpContextAccessor httpContextAccessor) : base(options, logger, encoder)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return Task.FromResult(AuthenticateResult.Fail("Http context in empty"));
        }

        var accessToken = httpContext.Session.GetString(SessionKey.AccessToken);
        var refreshToken = httpContext.Session.GetString(SessionKey.RefreshToken);

        if (accessToken is null || refreshToken is null)
        {
            return Task.FromResult(AuthenticateResult.Fail("Access token or refresh token is not found"));
        }

        var claims = new[]
        {
            new Claim(SessionKey.AccessToken, accessToken),
            new Claim(SessionKey.RefreshToken, refreshToken),
        };
        var claimsIdentity = new ClaimsIdentity(claims, nameof(CustomAuthenticationHandler));
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
