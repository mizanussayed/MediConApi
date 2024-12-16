using System.Data;
using System.Data.Common;

namespace MediCon.Core.Features.Users.Entity;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public static void MapFromDbWithReader(DbDataReader reader, User user)
    {
        user.Username = reader.GetString(reader.GetOrdinal("NAME"));
        user.Password = reader.GetString(reader.GetOrdinal("KEY"));
    }

    public static void MapFromDbWithDataRow(DataRow row, User user)
    {
        user.Id = (long)row.Field<decimal>("ID");
        user.Username = row.Field<string>("NAME") ?? string.Empty;
        user.Password = row.Field<string>("CODE") ?? string.Empty;
    }
}
