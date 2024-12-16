namespace MediCon.Core.Features.FPackages.Model;

public sealed class PackageResponseModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
    public decimal StartFromPrice { get; set; }
    public string? ShortDescription { get; set; }

}