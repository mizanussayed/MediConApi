using System.Data;
using System.Data.Common;

namespace MediCon.Core.Features.Users.Entity;

public class UserLoginInformation
{
    public string Msisdn { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int UserType { get; set; }
    public int UserOtherId { get; set; }

    public static void MapFromDbWithReader(DbDataReader reader, UserLoginInformation user)
    {
        user.Msisdn = reader.GetValue(reader.GetOrdinal("MSISDN")).ToString() ?? string.Empty;
        user.Code = reader.GetValue(reader.GetOrdinal("CODE")).ToString() ?? string.Empty;
        user.UserType = reader.IsDBNull(reader.GetOrdinal("USER_TYPE")) ? default : reader.GetByte("USER_TYPE");
        user.UserOtherId = reader.IsDBNull(reader.GetOrdinal("USER_OTHER_ID")) ? default : reader.GetInt32("USER_OTHER_ID");
    }
}
