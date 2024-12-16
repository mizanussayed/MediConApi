using Api.Configurations.Helpers;

using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Operators.Model;
using MediCon.Core.Features.Operators.Service;

using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.Api.Controllers.Operators;

public class OperatorApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Operator")
            .WithTags("Operator")
            .RequireAuthorization();

        group.MapGet("/Get", GetAllOperator)
            .WithOpenApi()
            .WithSummary("To get all operators.").WithDescription("This API will be used to get all operators.");

        group.MapGet("/GetById/{id}", GetOperator)
            .WithOpenApi()
            .WithSummary("To get a specific operator.").WithDescription("This API will be used to get filter wise operator.");

        group.MapPost("/Create", CreateOperator)
            .WithOpenApi()
            .WithSummary("To create a new operator.").WithDescription("This API will be used to create new operator.");

        group.MapPut("/Update/{id}", UpdateOperator)
            .WithOpenApi()
            .WithSummary("To update an existing operator.").WithDescription("This API will be used to update an existing operator.");

        group.MapDelete("/Delete/{id}", DeleteOperator)
            .WithOpenApi()
            .WithSummary("To delete an existing operator.").WithDescription("This API will be used to delete an existing operator.");
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<OperatorResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<OperatorResponseModel>>>>> GetAllOperator([FromServices] OperatorService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<OperatorResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<OperatorResponseModel>>, JsonHttpResult<ApiResponse<OperatorResponseModel>>>> GetOperator([FromServices] OperatorService service, long id, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<OperatorResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateOperator([FromServices] OperatorService service, HttpContext httpContext, OperatorRequestModel model, CancellationToken cancellationToken)
    {
        var currentUserInfo = httpContext.GetCurrentUserInfo();

        if (!currentUserInfo.Success)
        {
            return ApiResponseResult.Problem<Success>(Error.Failure("Could not retrieve current user info"));
        }
        var result = await service.CreateAsync(model, currentUserInfo.CurrentUserInfo.CurrentUserId, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateOperator([FromServices] OperatorService service, HttpContext httpContext, OperatorRequestModel model, long id, CancellationToken cancellationToken)
    {
        var currentUserInfo = httpContext.GetCurrentUserInfo();

        if (!currentUserInfo.Success)
        {
            return ApiResponseResult.Problem<Success>(Error.Failure("Could not retrieve current user info"));
        }
        var result = await service.UpdateAsync(model, id, currentUserInfo.CurrentUserInfo.CurrentUserId, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteOperator([FromServices] OperatorService service, HttpContext httpContext, int id, CancellationToken cancellationToken)
    {
        var currentUserInfo = httpContext.GetCurrentUserInfo();

        if (!currentUserInfo.Success)
        {
            return ApiResponseResult.Problem<Success>(Error.Failure("Could not retrieve current user info"));
        }

        var result = await service.DeleteAsync(id, currentUserInfo.CurrentUserInfo.CurrentUserId, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}