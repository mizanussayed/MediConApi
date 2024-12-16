namespace MediCon.WebUI.Configurations.Helper;

public interface IDateTimeHelper
{
    public DateTime UtcNow { get; }
    public DateTime Now { get; }
}
