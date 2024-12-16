namespace MediCon.Core.Configurations.Settings.ApiUrl;

public class ApiUrlSettings : ISettings
{
    public const string SectionName = "ApiUrls";

    public string JsonPlaceHolder { get; set; } = string.Empty;
    public LoginApiSettings? LoginApi { get; set; }
}
