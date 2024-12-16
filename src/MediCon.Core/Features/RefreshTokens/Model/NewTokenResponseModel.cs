namespace MediCon.Core.Features.RefreshTokens.Model;

public sealed class NewTokenResponseModel
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int AccessTokenExpireInMinutes { get; set; }
}
