using System.ComponentModel.DataAnnotations.Schema;

using MediCon.Core.Configurations.CommonModel;
using MediCon.Core.Features.Countries.Entity;


namespace MediCon.Core.Features.Cities.Entity;
public sealed class City : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    [ForeignKey(nameof(Country))]
    public long CountryId { get; set; }
    public Country Country { get; set; } = null!;
}
