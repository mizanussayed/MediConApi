using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Countries.Model;
using MediCon.Core.Features.Countries.Service;

namespace MediCon.Api.Controllers.Countries;

public class CountryApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/country").WithTags("Country");


        group.MapGet("/get-all-country", GetAllCountry)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-country/{id}", GetCountry);
        group.MapPost("/create-country", CreateCountry);
        group.MapPut("/update-country/{id}", UpdateCountry);
        group.MapDelete("/delete-country/{id}", DeleteCountry);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<CountryResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<CountryResponseModel>>>>> GetAllCountry([FromServices] CountryService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<CountryResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<CountryResponseModel>>, JsonHttpResult<ApiResponse<CountryResponseModel>>>> GetCountry([FromServices] CountryService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<CountryResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateCountry([FromServices] CountryService service, CountryRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateCountry([FromServices] CountryService service, CountryRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, 99, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteCountry([FromServices] CountryService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}