using MediCon.Core.Features.Cities.Entity;
using MediCon.Core.Features.Countries.Entity;
using MediCon.Core.Features.Doctors.Entity;
using MediCon.Core.Features.Facilities.Entity;
using MediCon.Core.Features.FPackages.Entity;
using MediCon.Core.Features.Hospitals.Entity;
using MediCon.Core.Features.Operators.Entity;
using MediCon.Core.Features.Specialities.Entity;
using MediCon.Core.Features.Treatments.Entity;

using Microsoft.EntityFrameworkCore;

namespace MediCon.Infrastructure.Configurations.DbContexts;

public class MediconcernDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=MediconcernDB;TrustServerCertificate=true;Integrated Security=True;");
    }
    public DbSet<Operator> Operators { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<City> Cities { get; set; } = default!;
    public DbSet<Hospital> Hospitals { get; set; } = default!;
    public DbSet<Speciality> Specialities { get; set; } = default!;
    public DbSet<Treatment> Treatments { get; set; } = default!;
    public DbSet<Doctor> Doctors { get; set; } = default!;
    public DbSet<Facility> Facilities { get; set; } = default!;
    public DbSet<Package> Packages { get; set; } = default!;
}