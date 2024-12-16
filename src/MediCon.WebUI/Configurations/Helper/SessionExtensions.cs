using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Services.Users.Models;

namespace MediCon.WebUI.Configurations.Helper;

public static class SessionExtensions
{
    [Obsolete("We use cookie for auth")]
    public static void SetUserInfo(this ISession session, UserLoginResponseModel userLoginResponseModel)
    {
        session.SetString(SessionKey.AccessToken, userLoginResponseModel.AccessToken);
        session.SetString(SessionKey.RefreshToken, userLoginResponseModel.RefreshToken);
    }
}
