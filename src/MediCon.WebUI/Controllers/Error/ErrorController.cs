using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Controllers.Error.ViewModels;
using MediCon.WebUI.Controllers.Home;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.WebUI.Controllers.Error;

[AllowAnonymous]
public class ErrorController : BaseController
{
    public IActionResult Handle(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status404NotFound => View("NotFound", ViewModel.Create("Page not found")),
            StatusCodes.Status403Forbidden => View("Forbidden", ViewModel.Create("Forbidden")),
            StatusCodes.Status401Unauthorized => RedirectToAction(nameof(HomeController.Login), nameof(HomeController).GetControllerName()),
            _ => View("UnknownError", ViewModel.Create("Unexpected error occurred")),
        };
    }

    public IActionResult HandleException()
    {
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature is null)
        {
            return View("UnknownError", UnknownErrorViewModel.Create("Unexpected error occurred", "Unexpected error occurred"));
        }

        var exception = exceptionHandlerPathFeature.Error;
        return View("UnknownError", UnknownErrorViewModel.Create("Unexpected error occurred", exception.Message));
    }

    public IActionResult Forbidden()
    {
        return View(ViewModel.Create("Forbidden"));
    }
}
