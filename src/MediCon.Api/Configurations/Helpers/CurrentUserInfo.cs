using System.Globalization;

using MediCon.Core.Configurations.CommonModel;
using MediCon.Core.Configurations.Helpers;

namespace Api.Configurations.Helpers;

public static class CurrentUserInfo
{
    private static readonly CommonRequestModel DefaultCurrentUserInfo = new();

    public static (bool Success, CommonRequestModel CurrentUserInfo) GetCurrentUserInfo(this HttpContext context)
    {
        var currentUserId = context.User.FindFirst(CustomClaimTypes.UserId)?.Value;
        if (string.IsNullOrWhiteSpace(currentUserId))
        {
            return (false, DefaultCurrentUserInfo);
        }

        if (!long.TryParse(currentUserId, NumberStyles.None, provider: null, out var userId) || userId == default)
        {
            return (false, DefaultCurrentUserInfo);
        }

        return (true, new CommonRequestModel
        {
            CurrentUserId = userId,
        });
    }

    public static bool SetCurrentUserInfo(this HttpContext context, CommonRequestModel model)
    {
        var currentUserInfo = context.GetCurrentUserInfo();
        if (!currentUserInfo.Success)
        {
            return currentUserInfo.Success;
        }

        model.CurrentUserId = currentUserInfo.CurrentUserInfo.CurrentUserId;

        return true;
    }
}
