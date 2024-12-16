using System.Data.Common;

using MediCon.Core.Configurations.CommonModel;

namespace MediCon.Core.Features.ServiceTypes.Entity;

public class ServiceType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public static void MapFromDbWithReader(DbDataReader reader, ServiceType serviceType)
    {
        serviceType.Id = reader.IsDBNull(reader.GetOrdinal("ID")) ? default : reader.GetInt64(reader.GetOrdinal("ID"));
        serviceType.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("NAME"));
    }
}