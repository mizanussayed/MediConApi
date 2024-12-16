using Api.Configurations.Helpers;

using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.ServiceTypes.Model;
using MediCon.Core.Features.ServiceTypes.Service;

using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MediCon.Api.Controllers.ServiceTypes;

public class ServiceTypeApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ServiceType")
            .WithTags("ServiceType")
            .RequireAuthorization();

        group.MapGet("/Get", GetAllServiceType)
            .WithOpenApi()
            .WithSummary("To get all service types.").WithDescription("This API will be used to get all service types.");

        group.MapGet("/GetById/{id}", GetServiceType)
            .WithOpenApi()
            .WithSummary("To get a specific service type.").WithDescription("This API will be used to get filter wise service type.");

        group.MapPost("/Create", CreateServiceType)
            .WithOpenApi()
            .WithSummary("To create a new service type.").WithDescription("This API will be used to create new service type.");

        group.MapPut("/Update/{id}", UpdateServiceType)
            .WithOpenApi()
            .WithSummary("To update an existing service type.").WithDescription("This API will be used to update an existing service type.");

        group.MapDelete("/Delete/{id}", DeleteServiceType)
            .WithOpenApi()
            .WithSummary("To delete an existing service type.").WithDescription("This API will be used to delete an existing service type.");
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<ServiceTypeResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<ServiceTypeResponseModel>>>>> GetAllServiceType([FromServices] ServiceTypeService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<ServiceTypeResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<ServiceTypeResponseModel>>, JsonHttpResult<ApiResponse<ServiceTypeResponseModel>>>> GetServiceType([FromServices] ServiceTypeService service, long id, CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<ServiceTypeResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateServiceType([FromServices] ServiceTypeService service, HttpContext httpContext, ServiceTypeRequestModel model, CancellationToken cancellationToken)
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

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateServiceType([FromServices] ServiceTypeService service, HttpContext httpContext, ServiceTypeRequestModel model, long id, CancellationToken cancellationToken)
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

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteServiceType([FromServices] ServiceTypeService service, HttpContext httpContext, int id, CancellationToken cancellationToken)
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