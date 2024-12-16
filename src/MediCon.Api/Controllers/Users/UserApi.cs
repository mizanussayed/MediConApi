using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading;

using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Users.Model;
using MediCon.Core.Features.Users.Service;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.Api.Controllers.Users;

public class UserApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/account");

        group.MapPost("login", LoginAsync).WithOpenApi().WithSummary("Login user").WithDescription("Returns refresh token");
        group.MapPost("token", TokenAsync).WithOpenApi().WithSummary("Swagger Login").WithDescription("Returns access token");
    }

    private async Task<Results<Ok<ApiResponse<UserLoginResponseModel>>, JsonHttpResult<ApiResponse<UserLoginResponseModel>>>> LoginAsync(
        [FromBody] UserLoginRequestModel request,
        [FromServices] UserService userService,
        CancellationToken cancellationToken)
    {
        var result = await userService.LoginAsync(request, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<UserLoginResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private async Task<IResult> TokenAsync(HttpContext httpContext, [FromServices] UserService userService, CancellationToken cancellationToken)
    {

        httpContext.Request.Form.TryGetValue("username", out var usernameValue);
        httpContext.Request.Form.TryGetValue("password", out var passwordValue);

        string username = usernameValue.ToString();
        string password = passwordValue.ToString();

        UserLoginRequestModel request = new UserLoginRequestModel()
        {
            UserName = username,
            Password = password

        };

        var result = await userService.LoginAsync(request, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return Results.Unauthorized();
        }
        else
        {
            return Results.Ok(new { access_token = result.Value.AccessToken });
        }
    }
}
