using System.Data.Common;

using MediCon.Core.Configurations.CommonModel;

namespace MediCon.Core.Features.Operators.Entity;
public class Operator : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string BinNumber { get; set; } = string.Empty;
    public string TinNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string POCNAME { get; set; } = string.Empty;
    public string POCDESIGNATION { get; set; } = string.Empty;
    public static void MapFromDbWithReader(DbDataReader reader, Operator entity)
    {
        entity.Id = reader.IsDBNull(reader.GetOrdinal("ID")) ? default : reader.GetInt64(reader.GetOrdinal("ID"));
        entity.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("NAME"));
        entity.BinNumber = reader.IsDBNull(reader.GetOrdinal("BINNUMBER")) ? string.Empty : reader.GetString(reader.GetOrdinal("BINNUMBER"));
        entity.TinNumber = reader.IsDBNull(reader.GetOrdinal("TINNUMBER")) ? string.Empty : reader.GetString(reader.GetOrdinal("TINNUMBER"));
        entity.Address = reader.IsDBNull(reader.GetOrdinal("ADDRESS")) ? string.Empty : reader.GetString(reader.GetOrdinal("ADDRESS"));
        //entity.POCNAME = reader.IsDBNull(reader.GetOrdinal("POCNAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("POCNAME"));
        //entity.POCDESIGNATION = reader.IsDBNull(reader.GetOrdinal("POCDESIGNATION")) ? string.Empty : reader.GetString(reader.GetOrdinal("POCDESIGNATION"));
    }
}