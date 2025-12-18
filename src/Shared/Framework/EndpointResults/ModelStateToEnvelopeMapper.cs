using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedKernel;

namespace Framework.EndpointResults;

public static class ModelStateToEnvelopeMapper
{
    public static IActionResult ToBadRequest(ModelStateDictionary modelState)
    {
        var errors = modelState
            .Where(x => x.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors.Select(e =>
            {
                var message = string.IsNullOrWhiteSpace(e.ErrorMessage)
                    ? "Invalid value."
                    : e.ErrorMessage;

                return Error.Validation(
                    code: "validation.error",
                    message: $"{x.Key}: {message}");
            }))
            .ToList();

        return new BadRequestObjectResult(
            Envelope.Error(new ErrorList(errors)));
    }
}