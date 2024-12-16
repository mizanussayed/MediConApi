using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.ServiceTypes.Models;

namespace MediCon.WebUI.Controllers.ServiceType.ViewModels;

public class ServiceTypeCreateViewModel : ViewModel
{
    public ServiceTypeRequestModel? ServiceTypeRequest { get; set; }
}
