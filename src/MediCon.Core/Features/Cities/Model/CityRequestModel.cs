namespace MediCon.Core.Features.Cities.Model;

public sealed class CityRequestModel
{
    public long Id { get; set; }
    public string Name { get;set; } = string.Empty; 
    public long CountryId { get;set; } 

}

