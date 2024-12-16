using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.Users.Models;

namespace MediCon.WebUI.Controllers.Home.ViewModels;

public class HomeLoginViewModel : ViewModel
{
    public string? ReturnURL { get; set; }
    public UserLoginRequestModel? UserLoginRequestModel { get; set; }
}
