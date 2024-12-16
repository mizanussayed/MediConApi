namespace MediCon.Core.Features.Users.Model;

public class UserLoginResponseModel
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int AccessTokenExpireInMinutes { get; set; }
    public required UserModel UserInfo { get; set; }
}

