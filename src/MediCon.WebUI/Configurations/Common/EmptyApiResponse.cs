namespace MediCon.WebUI.Configurations.Common;

public class EmptyApiResponse : ApiResponse<object?>
{
    public static EmptyApiResponse NoResponse(int statusCode = StatusCodes.Status500InternalServerError, string message = "Unable to get valid response")
    {
        return new EmptyApiResponse
        {
            Message = message,
            Status = statusCode,
            Data = default,
        };
    }

    public static EmptyApiResponse InvalidResponse(int statusCode, string message = "Unable to get valid response")
    {
        return new EmptyApiResponse()
        {
            Message = message,
            Status = statusCode,
            Data = default,
        };
    }

    public static EmptyApiResponse UnexpectedResponse(int statusCode = StatusCodes.Status499ClientClosedRequest, string message = "Unexpected error occurred")
    {
        return new EmptyApiResponse()
        {
            Message = message,
            Status = statusCode,
            Data = default,
        };
    }

    public static EmptyApiResponse UnAuthorized()
    {
        return new EmptyApiResponse()
        {
            Message = "Not logged in",
            Status = StatusCodes.Status401Unauthorized,
            Data = default,
        };
    }
}
