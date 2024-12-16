using System.Data.Common;

namespace MediCon.Core.Features.RefreshTokens.Entity;

public class RefreshToken
{
    public long Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpireTime { get; set; }
    public long UserId { get; set; }

    public bool IsExpired(DateTime currentTime) => currentTime > ExpireTime;

    public static void MapFromDbWithReader(DbDataReader reader, RefreshToken refreshToken)
    {
        refreshToken.Id = reader.GetInt64(reader.GetOrdinal("ID"));
        refreshToken.Token = reader.GetString(reader.GetOrdinal("TOKEN"));
        refreshToken.ExpireTime = reader.GetDateTime(reader.GetOrdinal("EXPIRE_TIME"));
        refreshToken.UserId = reader.GetInt64(reader.GetOrdinal("USER_ID"));
    }
}
