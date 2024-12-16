using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediCon.WebUI.Configurations.Helper;

public static class HtmlHelperExtensions
{
    public static string IsActive(this IHtmlHelper<dynamic> html, string controller, string action, string activeClass = "active")
    {
        var controllerName = html.ViewContext.RouteData.Values["controller"]?.ToString();
        var actionName = html.ViewContext.RouteData.Values["action"]?.ToString();

        var isControllerEqual = string.Equals(controller, controllerName, StringComparison.OrdinalIgnoreCase);
        var isActionEqual = string.Equals(action, actionName, StringComparison.OrdinalIgnoreCase);

        if (isControllerEqual && isActionEqual) return activeClass;
        return string.Empty;
    }
}
