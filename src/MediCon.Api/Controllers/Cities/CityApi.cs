using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Cities.Model;
using MediCon.Core.Features.Cities.Service;
using MediCon.Api.Configurations.Endpoints;

namespace MediCon.Api.Controllers.Cities;

public class CityApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/city").WithTags("City");


        group.MapGet("/get-all-city", GetAllCity)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-city/{id}", GetCity);
        group.MapPost("/create-city", CreateCity);
        group.MapPut("/update-city/{id}", UpdateCity);
        group.MapDelete("/delete-city/{id}", DeleteCity);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<CityResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<CityResponseModel>>>>> GetAllCity([FromServices] CityService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<CityResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<CityResponseModel>>, JsonHttpResult<ApiResponse<CityResponseModel>>>> GetCity([FromServices] CityService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<CityResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateCity([FromServices] CityService service, CityRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateCity([FromServices] CityService service, CityRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, 99, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteCity([FromServices] CityService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}