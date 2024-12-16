using System.Security.Claims;

using MediCon.Core.Configurations.Settings;

namespace MediCon.Core.Configurations.Helpers;

public interface IJwtTokenHelper
{
    JwtSettings JwtSettings { get; }
    string GenerateNewToken(IEnumerable<Claim> claims);
    Ulid GenerateNewRefreshToken();
}
