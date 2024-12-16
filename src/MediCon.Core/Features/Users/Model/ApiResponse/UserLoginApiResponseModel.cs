using System.Text.Json.Serialization;

namespace MediCon.Core.Features.Users.Model.ApiResponse;

#pragma warning disable MA0048

public class UserLoginApiResponseModel
{
    [JsonPropertyName("userID")] public decimal UserId { get; set; }
    [JsonPropertyName("userName")] public string LoginName { get; set; } = string.Empty;
    [JsonPropertyName("token")] public string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("refreshtoken")] public string RefreshToken { get; set; } = string.Empty;
    public int? PasswordChangePolicy { get; set; }
    public UserLoginResponseUser? User { get; set; }
}

public class UserLoginResponseUser
{
    public long UserId { get; set; }
    public int? UserType { get; set; }
    public string? FullName { get; set; }
    [JsonPropertyName("LoginName")] public string? Username { get; set; }
    public string? PasswordNeverExp { get; set; }
    public string? UserChangedPassword { get; set; }
    public string? MobileNumber { get; set; }
    public string? Email { get; set; }
}

public class UserLoginAPIResponse<T>
{
    public ExecutionState ExecutionState { get; set; }
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("is_success")]
    public bool IsSuccess { get; set; }
    public bool IsLocked { get; set; }
    public T? Data { get; set; } = default;
}

public enum ExecutionState
{
    Failure = 0,
    Success = 10,
}
