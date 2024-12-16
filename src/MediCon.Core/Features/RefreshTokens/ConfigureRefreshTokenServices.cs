using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.RefreshTokens.Model;
using MediCon.Core.Features.RefreshTokens.Service;
using MediCon.Core.Features.RefreshTokens.Validators;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Features.RefreshTokens;

internal class ConfigureRefreshTokenServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<RefreshTokenService>();

        services.AddScoped<IValidator<NewTokenRequestModel>, NewRefreshTokenValidator>();
    }
}
