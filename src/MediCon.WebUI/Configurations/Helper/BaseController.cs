using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Configurations.Pagination;
using MediCon.WebUI.Controllers.Error.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;

namespace MediCon.WebUI.Configurations.Helper;

[Authorize]
public class BaseController : Controller
{
    protected IActionResult ErrorView(string title, bool hasLayout = true)
    {
        var model = new KnownErrorViewModel { PageTitle = title, Title = title, HasLayout = hasLayout };
        return View("~/Views/Error/KnownError.cshtml", model);
    }

    protected IActionResult SuccessJson(object? data = default, string message = "Success")
    {
        return Ok(new ApiResponse<object>()
        {
            Message = message,
            Status = StatusCodes.Status200OK,
            Data = data,
        });
    }

    #region Problem JSON
    protected IActionResult ProblemJson(string message, object? data = default)
    {
        return Ok(new ApiResponse<object>()
        {
            Message = message,
            Status = StatusCodes.Status400BadRequest,
            Data = data,
        });
    }

    protected IActionResult ProblemJson<T>(ApiResponse<T> apiResponse)
    {
        return Ok(apiResponse);
    }

    protected IActionResult ProblemJson(EmptyApiResponse apiResponse)
    {
        return Ok(apiResponse);
    }
    #endregion

    protected IActionResult DatableJson<T>(PaginationResult<T>? paginationResult)
    {
        return Ok(new DataTablesResponseModel<T>
        {
            Data = paginationResult?.Data ?? [],
            TotalItems = paginationResult?.TotalItems ?? 0,
            TotalDisplayItems = paginationResult?.TotalItems ?? 0,
        });
    }
}
