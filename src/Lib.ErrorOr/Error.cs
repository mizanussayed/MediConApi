namespace Lib.ErrorOr;

public readonly record struct Error
{
    private static readonly Dictionary<string, string[]> EmptyValidationErrors = [];

    public string Message { get; }
    public int? ErrorCode { get; }
    public ErrorType Type { get; }

    public readonly IDictionary<string, string[]> ValidationErrors { get; init; } = EmptyValidationErrors;

    private Error(
        string message,
        ErrorType type,
        int? errorCode = null)
    {
        Message = message;
        Type = type;
        ErrorCode = errorCode;
    }

    public static Error Failure(
        string message = "A failure error has occurred",
        int? errorCode = null)
        => new(message, ErrorType.Failure, errorCode: errorCode);
    public static Error Unexpected(
        string message = "An unexpected error has occurred",
        int? errorCode = null)
        => new(message, ErrorType.Unexpected, errorCode: errorCode);
    public static Error Validation(
        string message = "One or more validation error has occurred",
        int? errorCode = null)
        => new(message, ErrorType.Validation, errorCode: errorCode);
    public static Error FieldValidation(
        IDictionary<string, string[]> validationErrors,
        string message = "One or more validation error has occurred",
        int? errorCode = null)
        => new(message, ErrorType.Validation, errorCode: errorCode)
        {
            ValidationErrors = validationErrors,
        };
    public static Error NotFound(
        string message = "Item is not found",
        int? errorCode = null)
        => new(message, ErrorType.NotFound, errorCode: errorCode);
    public static Error Unauthorized(
        string message = "You need to login first",
        int? errorCode = null)
        => new(message, ErrorType.Unauthorized, errorCode: errorCode);
    public static Error Forbidden(
        string message = "You are not allowed to perform this action",
        int? errorCode = null)
        => new(message, ErrorType.Forbidden, errorCode: errorCode);
    public static Error Custom(
        ErrorType type,
        string message,
        int? errorCode = null)
        => new(message, type, errorCode: errorCode);
}
