using MediCon.Core.Configurations.CommonModel;


namespace MediCon.Core.Features.Treatments.Entity;

public sealed class Treatment : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}
