using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Doctors.Model;
using MediCon.Core.Features.Doctors.Service;
using MediCon.Api.Configurations.Endpoints;

namespace MediCon.Api.Controllers.Doctors;

public class DoctorApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/doctor").WithTags("Doctor");


        group.MapGet("/get-all-doctor", GetAllDoctor)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-doctor/{id}", GetDoctor);
        group.MapPost("/create-doctor", CreateDoctor);
        group.MapPut("/update-doctor/{id}", UpdateDoctor);
        group.MapDelete("/delete-doctor/{id}", DeleteDoctor);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<DoctorResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<DoctorResponseModel>>>>> GetAllDoctor([FromServices] DoctorService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<DoctorResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<DoctorResponseModel>>, JsonHttpResult<ApiResponse<DoctorResponseModel>>>> GetDoctor([FromServices] DoctorService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<DoctorResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateDoctor([FromServices] DoctorService service, DoctorRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateDoctor([FromServices] DoctorService service, DoctorRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, 99, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteDoctor([FromServices] DoctorService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}