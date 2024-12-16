namespace MediCon.Core.Features.RefreshTokens.Model;

public sealed class NewTokenRequestModel
{
    public string RefreshToken { get; set; } = string.Empty;
}
