namespace MediCon.Core.Features.Facilities.Model;

public sealed class FacilityResponseModel
{
    public long Id { get; set; }
    public string Name { get;set; } = string.Empty; 
public string? Details { get;set; } 
    public long HospitalId { get;set; } 

}