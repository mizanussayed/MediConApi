using MediCon.WebUI.Services.Users.Models;

namespace MediCon.WebUI.Controllers.Home.ViewModels;

public class HomeLoginRequestViewModel : UserLoginRequestModel
{
    public string? ReturnURL { get; set; }
}
