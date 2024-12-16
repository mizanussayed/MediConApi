using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Facilities.Model;
using MediCon.Core.Features.Facilities.Service;
using MediCon.Api.Configurations.Endpoints;

namespace MediCon.Api.Controllers.Facilities;

public class FacilityApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/facility").WithTags("Facility");


        group.MapGet("/get-all-facility", GetAllFacility)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-facility/{id}", GetFacility);
        group.MapPost("/create-facility", CreateFacility);
        group.MapPut("/update-facility/{id}", UpdateFacility);
        group.MapDelete("/delete-facility/{id}", DeleteFacility);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<FacilityResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<FacilityResponseModel>>>>> GetAllFacility([FromServices] FacilityService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<FacilityResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<FacilityResponseModel>>, JsonHttpResult<ApiResponse<FacilityResponseModel>>>> GetFacility([FromServices] FacilityService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<FacilityResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateFacility([FromServices] FacilityService service, FacilityRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateFacility([FromServices] FacilityService service, FacilityRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, 99, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteFacility([FromServices] FacilityService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}