using Lib.DBAccess.Contexts;

namespace MediCon.Infrastructure.Configurations.DbContexts;

public class OracleDMSPhaseFourDbContext : OracleDbContext
{
    public OracleDMSPhaseFourDbContext(string connectionString) : base(connectionString)
    {
    }
}
