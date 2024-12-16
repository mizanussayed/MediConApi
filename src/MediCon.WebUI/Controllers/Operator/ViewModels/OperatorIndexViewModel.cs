using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.OperatorInfo.Models;

namespace MediCon.WebUI.Controllers.Operator.ViewModels
{
    public class OperatorIndexViewModel : ViewModel
    {
        public IList<OperatorInfoResponseModel> Operators { get; set; } = [];
    }
}