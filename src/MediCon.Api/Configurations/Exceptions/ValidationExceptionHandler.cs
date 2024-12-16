using MediCon.Api.Configurations.Response;
using MediCon.Core.Configurations.Helpers;

using FluentValidation;

using Microsoft.AspNetCore.Diagnostics;

namespace MediCon.Api.Configurations.Exceptions;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException fluentValidationException)
        {
            return false;
        }

        var dictionaryErrors = fluentValidationException.Errors.ToDictionaryErrors();

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(ApiResponse.ValidationProblem(dictionaryErrors), cancellationToken: cancellationToken).ConfigureAwait(false);

        return true;
    }
}
