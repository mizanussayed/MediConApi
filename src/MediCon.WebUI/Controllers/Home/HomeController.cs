using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediCon.WebUI.Configurations.Helper;
using MediCon.WebUI.Controllers.Home.ViewModels;
using MediCon.WebUI.Configurations.Constants;
using MediCon.WebUI.Services.Users.Services;
using System.Globalization;
using MediCon.WebUI.Services.Users.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using MediCon.WebUI.Services.Generals.Enum;

namespace MediCon.WebUI.Controllers.Home;

public class HomeController : BaseController
{
    private readonly IUserService _userService;
    private readonly IDateTimeHelper _dateTimeHelper;

    public HomeController(IUserService userService, IDateTimeHelper dateTimeHelper)
    {
        _userService = userService;
        _dateTimeHelper = dateTimeHelper;
    }

    public IActionResult Index()
    {
        return View(new HomeIndexViewModel { PageTitle = "Home" });
    }

    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = default)
    {
        return View(new HomeLoginViewModel
        {
            PageTitle = "Login",
            ReturnURL = returnUrl,
            UserLoginRequestModel = new UserLoginRequestModel()
        });
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] HomeLoginRequestViewModel model, CancellationToken cancellationToken)
    {
        var viewModel = new HomeLoginViewModel
        {
            PageTitle = "Login",
            ReturnURL = model?.ReturnURL,
            UserLoginRequestModel = model,
        };

        if (model is null)
        {
            return View(viewModel.WithError("Invalid request"));
        }

        var result = await _userService.Login(model, cancellationToken).ConfigureAwait(false);
        if (!result.Success)
        {
            return View(viewModel.WithError(result.Message));
        }

        var userLoginResponse = result.Data;
        if (userLoginResponse is null)
        {
            return View(viewModel.WithError("Could not retrieve user information"));
        }
        // Set session variable
        HttpContext.Session.SetString("UserName", userLoginResponse.UserInfo.UserName);
        
        if (userLoginResponse.UserInfo.UserName == "smhaque" ||
            userLoginResponse.UserInfo.UserName == "kamrulssan" ||
            userLoginResponse.UserInfo.UserName == "shossain_2" ||
            userLoginResponse.UserInfo.UserName == "ahaque")
        {
            HttpContext.Session.SetString("UserRole", ((long)UserTypeEnum.BLUser).ToString());
            //Define Approval Role
            if (string.Equals(userLoginResponse.UserInfo.UserName, "smhaque", StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Session.SetString("ApprovalRole", ((long)ApprovalTypeEnum.IT).ToString());
            }
            else if (userLoginResponse.UserInfo.UserName == "kamrulssan")
            {
                HttpContext.Session.SetString("ApprovalRole", ((long)ApprovalTypeEnum.RO).ToString());
            }
            else if (userLoginResponse.UserInfo.UserName == "shossain_2")
            {
                HttpContext.Session.SetString("ApprovalRole", ((long)ApprovalTypeEnum.Tax).ToString());
            }
            else if (userLoginResponse.UserInfo.UserName == "ahaque")
            {
                HttpContext.Session.SetString("ApprovalRole", ((long)ApprovalTypeEnum.Treasury).ToString());
            }
        }
        else if (userLoginResponse.UserInfo.UserName == "ar.islam")
        {
            HttpContext.Session.SetString("UserRole", ((long)UserTypeEnum.Operator).ToString());

            HttpContext.Session.SetString("OperatorId", "11");
        }
        else if (userLoginResponse.UserInfo.UserName == "fuzaman")
        {
            HttpContext.Session.SetString("UserRole", ((long)UserTypeEnum.Operator).ToString());

            HttpContext.Session.SetString("OperatorId", "12");
        }
        else if (userLoginResponse.UserInfo.UserName == "xxnasim")
        {
            HttpContext.Session.SetString("UserRole", ((long)UserTypeEnum.Distributor).ToString());

            HttpContext.Session.SetString("DistributorId", "261");
            HttpContext.Session.SetString("UserId", userLoginResponse.UserInfo.Id.ToString());
        }
        // Retrieve session variable
        //var userName = HttpContext.Session.GetString("UserName");

        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, userLoginResponse.UserInfo.Id.ToString(provider: null)),
            new(CustomClaimTypes.UserName, userLoginResponse.UserInfo.UserName),
            new(CustomClaimTypes.UserEmail, userLoginResponse.UserInfo.EmailAddress),
            new(CustomClaimTypes.AccessToken, userLoginResponse.AccessToken),
            new(CustomClaimTypes.RefreshToken, userLoginResponse.RefreshToken),
            new(CustomClaimTypes.ExpireTime, _dateTimeHelper.Now.AddMinutes(userLoginResponse.AccessTokenExpireInMinutes).ToString(CultureInfo.InvariantCulture)),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        var indexUrl = $"~/{nameof(HomeController).GetControllerName()}/{nameof(Index)}";
        if (!string.IsNullOrEmpty(model.ReturnURL) && Url.IsLocalUrl(model.ReturnURL))
        {
            return LocalRedirect(model.ReturnURL);
        }
        return LocalRedirect(indexUrl);
    }

    public async Task<IActionResult> Logout()
    {
        var loginUrl = $"~/{nameof(HomeController).GetControllerName()}/{nameof(Login)}";
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        HttpContext.Session.Clear();

        return LocalRedirect(loginUrl);
    }
}
