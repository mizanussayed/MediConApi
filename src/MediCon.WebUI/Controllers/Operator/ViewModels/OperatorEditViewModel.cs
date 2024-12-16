using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.OperatorInfo.Models;

namespace MediCon.WebUI.Controllers.Operator.ViewModels
{
    public class OperatorEditViewModel : ViewModel
    {
        public OperatorInfoResponseModel? OperatorResponse { get; set; }
        public OperatorInfoRequestModel? OperatorRequest { get; set; }
    }
}