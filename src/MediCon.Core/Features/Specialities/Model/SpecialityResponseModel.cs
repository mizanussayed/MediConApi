namespace MediCon.Core.Features.Specialities.Model;

public sealed class SpecialityResponseModel
{
    public long Id { get; set; }
    public string Name { get;set; } = string.Empty; 
public string? ShortDescription { get;set; } 
public string? Description { get;set; } 
public string? IconUrl { get;set; } 
public string? ImageUrl { get;set; } 

}