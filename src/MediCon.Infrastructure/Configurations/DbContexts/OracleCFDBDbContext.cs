using Lib.DBAccess.Contexts;

namespace MediCon.Infrastructure.Configurations.DbContexts;


public class OracleCFDBDbContext : OracleDbContext
{
    public OracleCFDBDbContext(string connectionString) : base(connectionString)
    {
    }
}
