namespace MediCon.WebUI.Configurations.Common;

public sealed class CurrentUserClaimInfo
{
    public long UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpireTime { get; set; }
}
