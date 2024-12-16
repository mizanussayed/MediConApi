using System.Globalization;

using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Constants;

namespace MediCon.WebUI.Configurations.Helper;

public static class CurrentUserInfo
{
    private static readonly CurrentUserClaimInfo DefaultCurrentUserInfo = new();

    public static (bool Success, CurrentUserClaimInfo CurrentUserInfo) GetCurrentUserInfo(this HttpContext context)
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

        var expireTimeString = context.User.FindFirst(CustomClaimTypes.ExpireTime)?.Value ?? string.Empty;
        DateTime.TryParse(expireTimeString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var expireTime);

        return (true, new CurrentUserClaimInfo
        {
            UserId = userId,
            UserName = context.User.FindFirst(CustomClaimTypes.UserName)?.Value ?? string.Empty,
            AccessToken = context.User.FindFirst(CustomClaimTypes.AccessToken)?.Value ?? string.Empty,
            RefreshToken = context.User.FindFirst(CustomClaimTypes.RefreshToken)?.Value ?? string.Empty,
            ExpireTime = expireTime,
        });
    }
}
