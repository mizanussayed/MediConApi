using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Hospitals.Model;
using MediCon.Core.Features.Hospitals.Service;

namespace MediCon.Api.Controllers.Hospitals;

public class HospitalApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/hospital").WithTags("Hospital");


        group.MapGet("/get-all-hospital", GetAllHospital)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-hospital/{id}", GetHospital);
        group.MapPost("/create-hospital", CreateHospital);
        group.MapPut("/update-hospital/{id}", UpdateHospital);
        group.MapDelete("/delete-hospital/{id}", DeleteHospital);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<HospitalResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<HospitalResponseModel>>>>> GetAllHospital([FromServices] HospitalService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<HospitalResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<HospitalResponseModel>>, JsonHttpResult<ApiResponse<HospitalResponseModel>>>> GetHospital([FromServices] HospitalService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<HospitalResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateHospital([FromServices] HospitalService service, HospitalRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateHospital([FromServices] HospitalService service, HospitalRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, 99, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteHospital([FromServices] HospitalService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}