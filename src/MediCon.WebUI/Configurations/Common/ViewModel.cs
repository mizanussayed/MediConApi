namespace MediCon.WebUI.Configurations.Common;

public class ViewModel
{
    public required string PageTitle { get; set; }
    public string? Description { get; set; }

    // Client side error
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public ShowErrorEnum ShowError { get; set; }

    public static ViewModel Create(string pageTitle, string? description = default) => new() { PageTitle = pageTitle, Description = description };
    public static ViewModel Error(string pageTitle, string errorMessage) => new() { PageTitle = pageTitle, IsError = true, ErrorMessage = errorMessage };
}
