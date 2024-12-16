using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.RefreshTokens.Model;
using MediCon.Core.Features.RefreshTokens.Service;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.Api.Controllers.RefreshToken;

public class RefreshTokenApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/RefreshToken").WithTags("RefreshToken");
        group.MapPost("GetNewRefreshToken", GetNewRefreshToken);
    }

    public async Task<Results<Ok<ApiResponse<NewTokenResponseModel>>, JsonHttpResult<ApiResponse<NewTokenResponseModel>>>> GetNewRefreshToken(
        [FromBody] NewTokenRequestModel requestModel,
        [FromServices] RefreshTokenService refreshTokenService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var result = await refreshTokenService.GenerateNewlyRefreshTokenAsync(requestModel.RefreshToken, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<NewTokenResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}
