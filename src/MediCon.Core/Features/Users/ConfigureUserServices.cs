using MediCon.Core.Configurations.Injector;
using MediCon.Core.Features.Users.Model;
using MediCon.Core.Features.Users.Service;
using MediCon.Core.Features.Users.Validators;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MediCon.Core.Features.Users;

internal class ConfigureUserServices : IInjectServices
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<UserExternalApiService>();

        services.AddScoped<IValidator<UserLoginRequestModel>, UserLoginRequestModelValidator>();
    }
}
