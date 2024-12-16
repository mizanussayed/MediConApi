using MediCon.Core.Configurations.CommonModel;


namespace MediCon.Core.Features.Packages.Entity;

public sealed class Package : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public decimal StartFromPrice { get; set; }
    public string ShortDescription { get; set; } = string.Empty;

}