using FluentValidation.Results;
using SharedKernel;

namespace Core.Validation;

public static class ValidationExtensions
{
    public static ErrorList ToList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;

        var errors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = TryDeserialize(errorMessage, validationError.PropertyName)
            select error;

        return errors.ToList();
    }

    private static Error TryDeserialize(string serialized, string propertyName)
    {
        try
        {
            var error = Error.Deserialize(serialized);
            var field = string.IsNullOrWhiteSpace(propertyName)
                ? ErrorField.Normalize(error.InvalidField)
                : ErrorField.Normalize(propertyName);

            return Error.Validation(error.Code, error.Message, field);
        }
        catch
        {
            return Error.Validation(
                "common.validation.invalid",
                serialized,
                ErrorField.Normalize(propertyName));
        }
    }
}
