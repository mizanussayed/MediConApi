using FluentValidation.Results;

using Lib.ErrorOr;

namespace MediCon.Core.Configurations.Helpers;

public static class ValidationHelper
{
    public static IDictionary<string, string[]> ToDictionaryErrors(this IEnumerable<ValidationFailure> validationFailures)
    {
        var errors = new Dictionary<string, string[]>(StringComparer.Ordinal);
        foreach (var error in validationFailures.DistinctBy(x => x.PropertyName))
        {
            errors.Add(error.PropertyName, [error.ErrorMessage]);
        }

        return errors;
    }

    public static Error ToError(this IEnumerable<ValidationFailure> validationFailures)
    {
        var errors = new Dictionary<string, string[]>(StringComparer.Ordinal);
        foreach (var error in validationFailures.DistinctBy(x => x.PropertyName))
        {
            errors.Add(error.PropertyName, [error.ErrorMessage]);
        }

        return Error.FieldValidation(errors);
    }
}
