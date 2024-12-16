using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Specialities.Model;
using MediCon.Core.Features.Specialities.Service;

namespace MediCon.Api.Controllers.Specialities;

public class SpecialityApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/speciality").WithTags("Speciality");


        group.MapGet("/get-all-speciality", GetAllSpeciality)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-speciality/{id}", GetSpeciality);
        group.MapPost("/create-speciality", CreateSpeciality);
        group.MapPut("/update-speciality/{id}", UpdateSpeciality);
        group.MapDelete("/delete-speciality/{id}", DeleteSpeciality);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<SpecialityResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<SpecialityResponseModel>>>>> GetAllSpeciality([FromServices] SpecialityService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<SpecialityResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<SpecialityResponseModel>>, JsonHttpResult<ApiResponse<SpecialityResponseModel>>>> GetSpeciality([FromServices] SpecialityService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<SpecialityResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateSpeciality([FromServices] SpecialityService service, SpecialityRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateSpeciality([FromServices] SpecialityService service, SpecialityRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteSpeciality([FromServices] SpecialityService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}