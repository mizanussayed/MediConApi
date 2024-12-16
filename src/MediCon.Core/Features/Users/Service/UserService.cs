using System.Security.Claims;

using MediCon.Core.Configurations.Helpers;
using MediCon.Core.Configurations.UnitOfWorks;
using MediCon.Core.Features.RefreshTokens.Service;
using MediCon.Core.Features.Users.Model;

using FluentValidation;

using Lib.ErrorOr;
using Serilog;
using Mapster;
using MediCon.Core.Features.Users.Model.ApiResponse;

namespace MediCon.Core.Features.Users.Service;

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenHelper _jwtTokenHelper;
    private readonly IValidator<UserLoginRequestModel> _userLoginRequestModelValidator;
    private readonly UserExternalApiService _userExternalApiService;
    private readonly RefreshTokenService _refreshTokenService;

    public UserService(
        IUnitOfWork unitOfWork,
        IJwtTokenHelper jwtTokenHelper,
        IValidator<UserLoginRequestModel> userLoginRequestModelValidator,
        UserExternalApiService userExternalApiService,
        RefreshTokenService refreshTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenHelper = jwtTokenHelper;
        _userLoginRequestModelValidator = userLoginRequestModelValidator;
        _userExternalApiService = userExternalApiService;
        _refreshTokenService = refreshTokenService;
    }

    public static IEnumerable<Claim> GetUserClaims(long userId)
    {
        return
        [
            new(CustomClaimTypes.UserId, value: userId.ToString(provider: null)),
        ];
    }

    public async Task<ErrorOr<UserLoginResponseModel>> LoginAsync(UserLoginRequestModel request, CancellationToken cancellationToken)
    {
        // Validate request
        var validateResult = await _userLoginRequestModelValidator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
        if (!validateResult.IsValid)
        {
            return validateResult.Errors.ToError();
        }

        // Call Login API
        var apiResult = await _userExternalApiService.Login(request.UserName, request.Password, cancellationToken).ConfigureAwait(false);
        if (apiResult.IsError)
        {
            return apiResult.Error;
        }

        // Check if Api login is successful
        var apiResponse = apiResult.Value;
        if (!apiResponse.IsSuccess)
        {
            return Error.Failure("Invalid credentials");
        }

        var apiUser = apiResponse.Data;
        if (apiUser is null)
        {
            Log.Error("User information is empty from API although user login API call is successful");
            return Error.Unexpected("Can not retrieve user information");
        }

        var userId = (long)apiUser.UserId;
        var refreshToken = await _refreshTokenService.GenerateNewRefreshTokenAsync(rememberMe: request.RememberMe, userId, cancellationToken).ConfigureAwait(false);
        if (refreshToken.IsError)
        {
            return refreshToken.Error;
        }

        var userInfo = apiUser.User.Adapt<UserModel>();
        var result = new UserLoginResponseModel
        {
            AccessToken = _jwtTokenHelper.GenerateNewToken(GetUserClaims(userId)),
            RefreshToken = refreshToken.Value,
            AccessTokenExpireInMinutes = _jwtTokenHelper.JwtSettings.AccessTokenExpireInMinutes,
            UserInfo = userInfo
        };

        return result;
    }

    //public async Task<ErrorOr<UserLoginResponseModel>> LoginAsync(UserLoginRequestModel request, CancellationToken cancellationToken)
    //{
    //    // Validate request
    //    var validateResult = await _userLoginRequestModelValidator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
    //    if (!validateResult.IsValid)
    //    {
    //        return validateResult.Errors.ToError();
    //    }
    //    UserLoginApiResponseModel apiResult = new UserLoginApiResponseModel
    //    {
    //        UserId = 100785,
    //        LoginName = "smhaque",
    //        AccessToken = "asdfasdfasgasgfdsfasfasdf",
    //        RefreshToken = "asdfasdfasdfasdfasdfasdfasdfasdfasdfasdfadsfasdf"
    //    };

    //    UserModel user = new UserModel
    //    {
    //        Id = (long)apiResult.UserId,
    //        FullName = "Test User",
    //        MobileNumber = "01733868944",
    //        EmailAddress = "abc@mail.com",
    //        UserName = apiResult.LoginName
    //    };

    //    // Check if Api login is successful
    //    var apiUser = apiResult;

    //    var userId = (long)apiUser.UserId;
    //    var refreshToken = await _refreshTokenService.GenerateNewRefreshTokenAsync(rememberMe: request.RememberMe, userId, cancellationToken).ConfigureAwait(false);
    //    if (refreshToken.IsError)
    //    {
    //        return refreshToken.Error;
    //    }
    //    var result = new UserLoginResponseModel
    //    {
    //        AccessToken = _jwtTokenHelper.GenerateNewToken(GetUserClaims(userId)),
    //        RefreshToken = refreshToken.Value,
    //        AccessTokenExpireInMinutes = _jwtTokenHelper.JwtSettings.AccessTokenExpireInMinutes,
    //        UserInfo = user
    //    };

    //    return result;
    //}

}
