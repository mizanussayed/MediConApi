using MediCon.Core.Features.Hospitals.Entity;
using MediCon.Core.Features.Operators.Entity;

using Lib.DBAccess.Contexts;

using Microsoft.EntityFrameworkCore;

namespace MediCon.Infrastructure.Configurations.DbContexts;




public class OracleCFDBDbContext : OracleDbContext
{
    public OracleCFDBDbContext(string connectionString) : base(connectionString)
    {
    }
}

public class OracleCFDBDbContext_copy : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=Mediconcern;TrustServerCertificate=true;Integrated Security=True;");

        //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Blogger;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
    }
    public DbSet<Operator> Operators { get; set; } = default!;
    public DbSet<Hospital> Hospitals { get; set; } = default!;
}