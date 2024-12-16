using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.ServiceTypes.Models;

namespace MediCon.WebUI.Controllers.ServiceType.ViewModels;

public class ServiceTypeEditViewModel : ViewModel
{
    public ServiceTypeResponseModel? ServiceTypeResponse { get; set; }
    public ServiceTypeRequestModel? ServiceTypeRequest { get; set; }
}
