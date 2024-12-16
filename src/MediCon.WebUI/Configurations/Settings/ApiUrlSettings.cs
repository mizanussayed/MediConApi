namespace MediCon.WebUI.Configurations.Settings;

public class ApiUrlSettings
{
    public const string SectionName = "ApiUrl";

    public string ApiBaseUrl { get; set; } = string.Empty;

    public string FileAPIBaseUrl { get; set; } = string.Empty;

    public static ApiUrlSettings Get(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);

        return section.Get<ApiUrlSettings>() ?? throw new Exception($"Could not load section: {SectionName} from appsettings");
    }
}
