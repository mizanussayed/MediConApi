namespace MediCon.Core.Features.Hospitals.Model;

public sealed class HospitalResponseModel
{
    public long Id { get; set; }
    public string Name { get;set; } = string.Empty; 
public string? IconUrl { get;set; } 
public string? ImageUrl { get;set; } 
public string? Location { get;set; } 
public string? Address { get;set; } 
public string? Phone { get;set; } 
public string? Email { get;set; } 
public string? Website { get;set; } 
    public long CityId { get;set; } 
    public long SpecialityId { get;set; } 
    public long TreatmentId { get;set; } 

}