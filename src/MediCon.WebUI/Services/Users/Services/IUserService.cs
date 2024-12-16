using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.Users.Models;

namespace MediCon.WebUI.Services.Users.Services;

public interface IUserService
{
    Task<ApiResponse<UserLoginResponseModel>> Login(UserLoginRequestModel model, CancellationToken cancellationToken = default);
}
