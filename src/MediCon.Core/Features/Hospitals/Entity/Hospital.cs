using System.ComponentModel.DataAnnotations.Schema;

using MediCon.Core.Configurations.CommonModel;
using MediCon.Core.Features.Cities.Entity;
using MediCon.Core.Features.Specialities.Entity;
using MediCon.Core.Features.Treatments.Entity;


namespace MediCon.Core.Features.Hospitals.Entity;

public sealed class Hospital : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;

    [ForeignKey(nameof(City))]
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    [ForeignKey(nameof(Speciality))]
    public int SpecialityId { get; set; }
    public Speciality Speciality { get; set; } = null!;
    [ForeignKey(nameof(Treatment))]
    public int TreatmentId { get; set; }
    public Treatment Treatment { get; set; } = null!;
}
