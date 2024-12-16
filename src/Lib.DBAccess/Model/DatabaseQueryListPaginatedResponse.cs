using MediCon.Core.Configurations.Pagination;

namespace Lib.DBAccess.Model;

public class DatabaseQueryListPaginatedResponse<T>
{
    public bool Success { get; internal set; }
    public int ErrorCode { get; internal set; }
    public string ErrorMessage { get; internal set; } = string.Empty;
    public PaginationResult<T> PaginatedData { get; set; } = default!;
}

public sealed class DatabaseQueryListPaginatedResponse
{
    public static DatabaseQueryListPaginatedResponse<Q> Failure<Q>(string errorMessage) where Q : class, new()
    {
        return new()
        {
            Success = false,
            ErrorMessage = errorMessage,
            PaginatedData = default!,
        };
    }
}

