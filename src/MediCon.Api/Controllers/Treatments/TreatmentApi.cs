using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.Treatments.Model;
using MediCon.Core.Features.Treatments.Service;
using MediCon.Api.Configurations.Endpoints;

namespace MediCon.Api.Controllers.Treatments;

public class TreatmentApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/treatment").WithTags("Treatment");


        group.MapGet("/get-all-treatment", GetAllTreatment)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-treatment/{id}", GetTreatment);
        group.MapPost("/create-treatment", CreateTreatment);
        group.MapPut("/update-treatment/{id}", UpdateTreatment);
        group.MapDelete("/delete-treatment/{id}", DeleteTreatment);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<TreatmentResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<TreatmentResponseModel>>>>> GetAllTreatment([FromServices] TreatmentService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<TreatmentResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<TreatmentResponseModel>>, JsonHttpResult<ApiResponse<TreatmentResponseModel>>>> GetTreatment([FromServices] TreatmentService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<TreatmentResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreateTreatment([FromServices] TreatmentService service, TreatmentRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdateTreatment([FromServices] TreatmentService service, TreatmentRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeleteTreatment([FromServices] TreatmentService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}