namespace MediCon.WebUI.Configurations.Helper;

public static class DateTimeExtension
{
    public static string ToUIDate(this DateTime? dateTime)
    {
        return dateTime is not DateTime validDateTime ? string.Empty : validDateTime.ToString("yyyy-MM-dd", provider: null);
    }

    public static string ToUIDate(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd", provider: null);
    }

    public static string ToUITime(this DateTime dateTime)
    {
        return dateTime.ToString("hh:mm tt", provider: null);
    }
}
