using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.RefreshTokens.Models;

namespace MediCon.WebUI.Services.RefreshTokens.Services;

public interface IRefreshTokenService
{
    Task<ApiResponse<NewTokenResponseModel>> GetNewToken(NewTokenRequestModel model, CancellationToken cancellationToken = default);
}
