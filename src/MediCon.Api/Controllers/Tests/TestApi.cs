using Api.Configurations.Helpers;

using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Tests.Entity;
using MediCon.Core.Features.Tests.Service;

using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.Api.Controllers.Tests;

public class TestApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Test").WithTags("Test");

        group.MapGet("get-application-user", GetApplication).WithOpenApi().WithDescription("Get application user");
        group.MapGet("get-all-productTests", GetAllProductTestAsync);
        group.MapGet("get-vendors", GetVendors);
        group.MapGet("create-market-visit", CreateMarketVisit);
        group.MapGet("get-posts", GetPostsAsync);
    }

    private static async Task<Results<Ok<ApiResponse<ApplicationUser>>, JsonHttpResult<ApiResponse<ApplicationUser>>>> GetApplication([FromServices] TestService userService, HttpContext httpContext, CancellationToken cancellationToken)
    {
        var currentUserInfo = httpContext.GetCurrentUserInfo();
        if (!currentUserInfo.Success)
        {
            return ApiResponseResult.Problem<ApplicationUser>(Error.Failure("Could not retrieve current user info"));
        }

        var result = await userService.GetApplicationUserAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<ApplicationUser>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private async Task<Results<Ok<ApiResponse<IEnumerable<ProductTest>>>, JsonHttpResult<ApiResponse<IEnumerable<ProductTest>>>>> GetAllProductTestAsync([FromServices] TestService testService, CancellationToken cancellationToken)
    {
        var result = await testService.GetAllProductTestAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<ProductTest>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private async Task<Results<Ok<ApiResponse<IEnumerable<Vendor>>>, JsonHttpResult<ApiResponse<IEnumerable<Vendor>>>>> GetVendors([FromServices] TestService testService, CancellationToken cancellationToken)
    {
        var result = await testService.GetVendors(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<Vendor>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private async Task<Results<Ok<ApiResponse<bool>>, JsonHttpResult<ApiResponse<bool>>>> CreateMarketVisit([FromServices] TestService testService, CancellationToken cancellationToken)
    {
        var result = await testService.CreateMarketVisit(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<bool>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private async Task<Results<Ok<ApiResponse<IEnumerable<Post>>>, JsonHttpResult<ApiResponse<IEnumerable<Post>>>>> GetPostsAsync([FromServices] TestService testService, CancellationToken cancellationToken)
    {
        var result = await testService.GetPostsAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<Post>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}
