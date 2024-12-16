using System.ComponentModel.DataAnnotations.Schema;

using MediCon.Core.Configurations.CommonModel;
using MediCon.Core.Features.Hospitals.Entity;


namespace MediCon.Core.Features.Facilities.Entity;

public sealed class Facility : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;

    [ForeignKey(nameof(Hospital))]
    public long HospitalId { get; set; }
    public Hospital Hospital { get; set; } = null!;
}
