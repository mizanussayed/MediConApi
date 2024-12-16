namespace MediCon.Core.Features.Doctors.Model;

public sealed class DoctorRequestModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Designation { get; set; }
    public string? Degree { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string? ProfileImageUrl { get; set; }

}

