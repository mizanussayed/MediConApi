using MediCon.WebUI.Configurations.Common;

namespace MediCon.WebUI.Controllers.Error.ViewModels;

public class UnknownErrorViewModel : ViewModel
{
    public required string Title { get; set; }
    public string? Message { get; set; }

    public static UnknownErrorViewModel Create(string pageTitle, string title, string? message = default)
    {
        return new UnknownErrorViewModel
        {
            PageTitle = pageTitle,
            Title = title,
            Message = message,
        };
    }
}
