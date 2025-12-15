using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth;

public interface ITwoFactorService
{
    Task<Result<string, ErrorList>> GenerateTwoFactorSecretAsync(
        Guid userId,
        CancellationToken ct);

    Task<Result<Guid, ErrorList>> EnableTwoFactorAsync(
        Guid userId, string code,
        CancellationToken ct);

    Task<Result<Guid, ErrorList>> DisableTwoFactorAsync(
        Guid userId,
        CancellationToken ct);
}