namespace Lib.DBAccess.Model;

public sealed class DatabaseQueryScalarResponse<T> where T : struct
{
    public bool Success { get; internal set; }
    public int ErrorCode { get; internal set; }
    public string ErrorMessage { get; internal set; } = string.Empty;
    public T Data { get; internal set; } = default!;
}

public sealed class DatabaseQueryScalarResponse
{
    public static DatabaseQueryScalarResponse<Q> Failure<Q>(string errorMessage) where Q : struct
    {
        return new()
        {
            Success = false,
            ErrorMessage = errorMessage,
            Data = default!,
        };
    }
}
