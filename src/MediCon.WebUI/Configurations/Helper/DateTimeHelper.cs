namespace MediCon.WebUI.Configurations.Helper;

public class DateTimeHelper : IDateTimeHelper
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}
