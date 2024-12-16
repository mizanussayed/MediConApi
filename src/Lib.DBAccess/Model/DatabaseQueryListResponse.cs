namespace Lib.DBAccess.Model;

public sealed class DatabaseQueryListResponse<T> where T : class, new()
{
    public bool Success { get; internal set; }
    public int ErrorCode { get; internal set; }
    public string ErrorMessage { get; internal set; } = string.Empty;
    public T Data { get; set; } = default!;
}

public sealed class DatabaseQueryListResponse
{
    public static DatabaseQueryListResponse<Q> Failure<Q>(string errorMessage) where Q : class, new()
    {
        return new()
        {
            Success = false,
            ErrorMessage = errorMessage,
            Data = default!,
        };
    }
}
