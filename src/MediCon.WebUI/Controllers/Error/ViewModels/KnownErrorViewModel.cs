using MediCon.WebUI.Configurations.Common;

namespace MediCon.WebUI.Controllers.Error.ViewModels;

public class KnownErrorViewModel : ViewModel
{
    public required string Title { get; set; }
    public bool HasLayout { get; set; }
}
