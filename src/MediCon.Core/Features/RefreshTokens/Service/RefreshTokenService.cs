using MediCon.Core.Configurations.Helpers;
using MediCon.Core.Configurations.Settings;
using MediCon.Core.Configurations.UnitOfWorks;
using MediCon.Core.Features.RefreshTokens.Entity;
using MediCon.Core.Features.RefreshTokens.Model;
using MediCon.Core.Features.RefreshTokens.Repository;
using MediCon.Core.Features.Users.Service;

using Lib.ErrorOr;

namespace MediCon.Core.Features.RefreshTokens.Service;

public class RefreshTokenService(
    IUnitOfWork unitOfWork,
    ISettingsHelper settingsHelper,
    IDateTimeHelper dateTimeHelper,
    IJwtTokenHelper jwtTokenHelper)
{
    public async Task<ErrorOr<string>> GenerateNewRefreshTokenAsync(bool rememberMe, long userId, CancellationToken cancellationToken)
    {
        var jwtSettings = settingsHelper.Get<JwtSettings>(JwtSettings.SectionName);

        var token = jwtTokenHelper.GenerateNewRefreshToken().ToString();
        var expireTime = dateTimeHelper.Now.AddHours(rememberMe ? jwtSettings.RefreshTokenExpireInHourIfRememberMe : jwtSettings.RefreshTokenExpireInHourIfNotRememberMe);

        var result = await CreateRefreshTokenAsync(token, expireTime, userId, cancellationToken).ConfigureAwait(false);
        if (result.IsError)
        {
            return result.Error;
        }

        return token;
    }

    public async Task<ErrorOr<NewTokenResponseModel>> GenerateNewlyRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        var refreshTokenResult = await GetRefreshTokenByTokenAsync(token, cancellationToken).ConfigureAwait(false);
        if (refreshTokenResult.IsError)
        {
            return refreshTokenResult.Error;
        }

        var settings = settingsHelper.Get<JwtSettings>(JwtSettings.SectionName);
        var refreshToken = refreshTokenResult.Value;
        var currentTime = dateTimeHelper.Now;
        var remainingExpireTime = currentTime - refreshTokenResult.Value.ExpireTime;
        var timeBeforeRefreshTokenExpiration = settings.TimeBeforeRefreshTokenExpirationInHour;

        if (remainingExpireTime.Duration() <= TimeSpan.FromHours(timeBeforeRefreshTokenExpiration))
        {
            refreshToken.ExpireTime = currentTime.AddHours(timeBeforeRefreshTokenExpiration);

            var result = await UpdateRefreshTokenAsync(refreshToken.Id, refreshToken.Token, refreshToken.ExpireTime, cancellationToken).ConfigureAwait(false);
            if (result.IsError)
            {
                return result.Error;
            }
        }

        var claims = UserService.GetUserClaims(userId: refreshToken.UserId);
        var newAccessToken = jwtTokenHelper.GenerateNewToken(claims);

        return new NewTokenResponseModel
        {
            AccessToken = newAccessToken,
            RefreshToken = token,
            AccessTokenExpireInMinutes = settings.AccessTokenExpireInMinutes,
        };
    }

    public async Task<ErrorOr<bool>> CreateRefreshTokenAsync(string token, DateTime expireTime, long userId, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IRefreshTokenRepository>();
        return await repository.CreateRefreshTokenAsync(token, expireTime, userId, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ErrorOr<bool>> DeleteRefreshTokenAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IRefreshTokenRepository>();
        return await repository.DeleteRefreshTokenAsync(id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ErrorOr<RefreshToken>> GetRefreshTokenByIdAsync(long id, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IRefreshTokenRepository>();
        return await repository.GetRefreshTokenByIdAsync(id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ErrorOr<RefreshToken>> GetRefreshTokenByTokenAsync(string token, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IRefreshTokenRepository>();
        return await repository.GetRefreshTokenByTokenAsync(token, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ErrorOr<bool>> UpdateRefreshTokenAsync(long id, string token, DateTime expireTime, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<IRefreshTokenRepository>();
        return await repository.UpdateRefreshTokenAsync(id, token, expireTime, cancellationToken).ConfigureAwait(false);
    }
}
