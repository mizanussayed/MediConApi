using System.ComponentModel.DataAnnotations;

namespace MediCon.WebUI.Services.Users.Models;

public class UserLoginRequestModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}
