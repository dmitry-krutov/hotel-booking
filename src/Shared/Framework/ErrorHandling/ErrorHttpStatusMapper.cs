using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Framework.ErrorHandling;

public static class ErrorHttpStatusMapper
{
    public static int Map(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
            ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
            ErrorType.CONFLICT or ErrorType.ALREADY_EXISTS => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
}
