using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Services.Users.Models;

namespace MediCon.WebUI.Services.Users.Services;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;

    #region URL
    private const string LoginURL = "api/Account/Login";
    #endregion

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ApiResponse<UserLoginResponseModel>> Login(UserLoginRequestModel model, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientKey.ApiNoAuth);
        var response = await client.PostAsJsonAsync(LoginURL, model, cancellationToken: cancellationToken).ConfigureAwait(false);
        return await response.GetApiResponseFromJsonAsync<UserLoginResponseModel>(cancellationToken: cancellationToken).ConfigureAwait(false);

        ////==Only Check Start
        //UserLoginResponseModel responseModel = new UserLoginResponseModel();
        //responseModel.AccessToken = "abc";
        //responseModel.RefreshToken = "bcd";
        //responseModel.AccessTokenExpireInMinutes = 100;
        //responseModel.UserInfo = new UserModel();
        //responseModel.UserInfo.Id = 10282;
        //responseModel.UserInfo.UserName = "smhaque";
        //responseModel.UserInfo.EmailAddress = "efg";
        //responseModel.UserInfo.FullName = "cde";
        //responseModel.UserInfo.MobileNumber = "098765";
        ////response.Data = responseModel;
        //var r = new ApiResponse<UserLoginResponseModel>();
        //r.Status = 200;
        //r.Data = responseModel;
        //return r;
        //==Only Check End
    }
}
