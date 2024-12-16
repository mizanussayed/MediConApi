namespace MediCon.WebUI.Services.Users.Models;

public class UserLoginResponseModel
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int AccessTokenExpireInMinutes { get; set; }
    //public required UserModel UserInfo { get; set; }
    public  UserModel UserInfo { get; set; } = new UserModel();
}
