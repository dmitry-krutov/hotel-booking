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
            let error = Error.Deserialize(errorMessage)
            let field = string.IsNullOrWhiteSpace(validationError.PropertyName)
                ? ErrorField.Normalize(error.InvalidField)
                : ErrorField.Normalize(validationError.PropertyName)
            select Error.Validation(error.Code, error.Message, field);

        return errors.ToList();
    }
}
