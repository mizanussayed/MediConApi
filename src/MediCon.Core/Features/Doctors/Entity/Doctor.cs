using MediCon.Core.Configurations.CommonModel;


namespace MediCon.Core.Features.Doctors.Entity;

public sealed class Doctor : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
}
