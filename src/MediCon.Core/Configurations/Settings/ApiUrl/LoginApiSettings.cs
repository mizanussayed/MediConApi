namespace MediCon.Core.Configurations.Settings.ApiUrl;

public class LoginApiSettings : ISettings
{
    public static readonly string SectionName = $"{ApiUrlSettings.SectionName}:{nameof(ApiUrlSettings.LoginApi)}";

    public string Url { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public string ApplicationKey { get; set; } = string.Empty;
}
