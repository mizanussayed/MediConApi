namespace MediCon.WebUI.Configurations.Helper;

public static class ControllerExtensions
{
    public static string GetControllerName(this string controllerName)
        => controllerName.Replace("Controller", "");
}
