namespace MediCon.Api.Configurations.Response;

public class ApiResponse<T>
{
    public string? Message { get; set; }
    public int Status { get; set; }
    public T? Data { get; set; } = default;
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(StringComparer.Ordinal);
}

public static class ApiResponse
{
    public static ApiResponse<T> Create<T>(string message, int statusCode, T? data = default)
    {
        return new ApiResponse<T>()
        {
            Message = message,
            Status = statusCode,
            Data = data,
        };
    }

    public static IResult SuccessResult<T>(T? data = default, string? message = "Success")
    {
        return new ProblemResult<T>(new ApiResponse<T>()
        {
            Message = message,
            Status = StatusCodes.Status200OK,
            Data = data,
        });
    }

    public static IResult ProblemResult<T>(int statusCode, string message = "Error occurred")
    {
        return new ProblemResult<T>(new ApiResponse<T>()
        {
            Message = message,
            Status = statusCode,
            Data = default,
        });
    }

    public static IResult ValidationProblemResult<T>(IDictionary<string, string[]> errors, string message = "Validation error occurred")
    {
        return new ProblemResult<T>(new ApiResponse<T>()
        {
            Message = message,
            Status = StatusCodes.Status400BadRequest,
            Data = default,
            Errors = errors,
        });
    }

    #region Empty API response
    public static EmptyApiResponse Create(string message, int statusCode)
    {
        return new EmptyApiResponse()
        {
            Message = message,
            Status = statusCode,
        };
    }

    public static IResult SuccessResult(string message = "Success")
    {
        return new ProblemResult<EmptyApiResponse>(new ApiResponse<EmptyApiResponse>()
        {
            Message = message,
            Status = StatusCodes.Status200OK,
        });
    }

    public static IResult ProblemResult(string message, int statusCode)
    {
        return new ProblemResult<EmptyApiResponse>(new ApiResponse<EmptyApiResponse>()
        {
            Message = message ?? "Error occurred",
            Status = statusCode,
            Data = default,
        });
    }

    public static IResult ValidationProblemResult(IDictionary<string, string[]> errors, string? message = null)
    {
        return new ProblemResult<EmptyApiResponse>(new ApiResponse<EmptyApiResponse>()
        {
            Message = message ?? "Validation error occurred",
            Status = StatusCodes.Status400BadRequest,
            Data = default,
            Errors = errors,
        });
    }

    public static ApiResponse<EmptyApiResponse> ValidationProblem(IDictionary<string, string[]> errors, string? message = null)
    {
        return new ApiResponse<EmptyApiResponse>()
        {
            Message = message ?? "Validation error occurred",
            Status = StatusCodes.Status400BadRequest,
            Data = default,
            Errors = errors,
        };
    }
    #endregion
}
