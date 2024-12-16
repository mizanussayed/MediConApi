using Lib.ErrorOr;

using Microsoft.AspNetCore.Http.HttpResults;

namespace MediCon.Api.Configurations.Response;

public class EmptyApiResponseResult
{
    public static Ok<EmptyApiResponse> Success(string message = "Success")
    {
        return TypedResults.Ok(new EmptyApiResponse()
        {
            Message = message,
            Status = StatusCodes.Status200OK,
        });
    }

    public static NotFound<EmptyApiResponse> NotFound<T>(string message = "Not found")
    {
        return TypedResults.NotFound(new EmptyApiResponse()
        {
            Message = message,
            Status = StatusCodes.Status404NotFound,
        });
    }

    public static JsonHttpResult<EmptyApiResponse> Problem<T>(
        string message = "Error occurred",
        int statusCode = StatusCodes.Status400BadRequest)
    {
        return TypedResults.Json(new EmptyApiResponse()
        {
            Message = message,
            Status = statusCode,
        }, statusCode: statusCode);
    }

    public static JsonHttpResult<EmptyApiResponse> Problem<T>(
        Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return TypedResults.Json(new EmptyApiResponse()
        {
            Message = error.Message,
            Status = statusCode,
        }, statusCode: statusCode);
    }

    public static BadRequest<EmptyApiResponse> ValidationProblem<T>(
        IDictionary<string, string[]> errors,
        string message = "Validation error occurred")
    {
        return TypedResults.BadRequest(new EmptyApiResponse()
        {
            Message = message,
            Status = StatusCodes.Status400BadRequest,
            Errors = errors,
        });
    }
}
