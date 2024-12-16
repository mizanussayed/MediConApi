using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.OperatorInfo.Models;

namespace MediCon.WebUI.Controllers.Operator.ViewModels
{
    public class OperatorCreateViewModel : ViewModel
    {
        public OperatorInfoRequestModel? OperatorRequest { get; set; }
    }
}