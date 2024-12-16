namespace MediCon.WebUI.Configurations.Common;

public class ApiResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; }
    public bool Success => Status == StatusCodes.Status200OK;
    public T? Data { get; set; } = default;
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(StringComparer.Ordinal);
    public IEnumerable<string> ErrorDetails => Errors.Values.SelectMany(v => v);
}

public static class ApiResponse
{
    public static ApiResponse<T> NoResponse<T>(int statusCode = StatusCodes.Status500InternalServerError, string message = "Unable to get valid response")
    {
        return new ApiResponse<T>()
        {
            Message = message,
            Status = statusCode,
            Data = default,
        };
    }

    public static ApiResponse<T> InvalidResponse<T>(int statusCode, string message = "Unable to get valid response")
    {
        return new ApiResponse<T>()
        {
            Message = message,
            Status = statusCode,
            Data = default,
        };
    }

    public static ApiResponse<T> UnexpectedResponse<T>(int statusCode = StatusCodes.Status499ClientClosedRequest, string message = "Unexpected error occurred")
    {
        return new ApiResponse<T>()
        {
            Message = message,
            Status = statusCode,
            Data = default,
        };
    }

    public static ApiResponse<T> UnAuthorized<T>()
    {
        return new ApiResponse<T>()
        {
            Message = "Not logged in",
            Status = StatusCodes.Status401Unauthorized,
            Data = default,
        };
    }
}
