using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.ServiceTypes.Models;

namespace MediCon.WebUI.Controllers.ServiceType.ViewModels;

public class ServiceTypeIndexViewModel : ViewModel
{
    public IList<ServiceTypeResponseModel> ServiceTypes { get; set; } = [];
}
