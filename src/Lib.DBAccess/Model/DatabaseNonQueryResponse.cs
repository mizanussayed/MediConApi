namespace Lib.DBAccess.Model;

public class DatabaseNonQueryResponse
{
    public bool Success { get; internal set; }
    public int ErrorCode { get; internal set; }
    public string ErrorMessage { get; internal set; } = string.Empty;

    public static DatabaseNonQueryResponse Failure(string errorMessage)
    {
        return new()
        {
            Success = false,
            ErrorMessage = errorMessage,
        };
    }
}
