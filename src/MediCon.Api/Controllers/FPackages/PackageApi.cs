using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MediCon.Api.Configurations.Endpoints;
using MediCon.Api.Configurations.Response;
using MediCon.Core.Features.FPackages.Model;
using MediCon.Core.Features.FPackages.Service;

namespace MediCon.Api.Controllers.FPackages;

public class PackageApi : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/package").WithTags("Package");


        group.MapGet("/get-all-package", GetAllPackage)
            .WithOpenApi()
            .WithSummary("all version");

        group.MapGet("/get-package/{id}", GetPackage);
        group.MapPost("/create-package", CreatePackage);
        group.MapPut("/update-package/{id}", UpdatePackage);
        group.MapDelete("/delete-package/{id}", DeletePackage);
    }

    private static async Task<Results<Ok<ApiResponse<IEnumerable<PackageResponseModel>>>, JsonHttpResult<ApiResponse<IEnumerable<PackageResponseModel>>>>> GetAllPackage([FromServices] PackageService service, CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<IEnumerable<PackageResponseModel>>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
    private static async Task<Results<Ok<ApiResponse<PackageResponseModel>>, JsonHttpResult<ApiResponse<PackageResponseModel>>>> GetPackage([FromServices] PackageService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<PackageResponseModel>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> CreatePackage([FromServices] PackageService service, PackageRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.CreateAsync(model, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> UpdatePackage([FromServices] PackageService service, PackageRequestModel model, CancellationToken cancellationToken)
    {
        var result = await service.UpdateAsync(model, 99, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }

    private static async Task<Results<Ok<ApiResponse<Success>>, JsonHttpResult<ApiResponse<Success>>>> DeletePackage([FromServices] PackageService service, int id, CancellationToken cancellationToken)
    {
        var result = await service.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return ApiResponseResult.Problem<Success>(result.Error);
        }

        return ApiResponseResult.Success(result.Value);
    }
}