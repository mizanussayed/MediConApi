namespace MediCon.WebUI.Services.Users.Models;

public class UserModel
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
}
