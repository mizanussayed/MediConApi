namespace MediCon.Core.Features.Doctors.Model;

public sealed class DoctorResponseModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
}