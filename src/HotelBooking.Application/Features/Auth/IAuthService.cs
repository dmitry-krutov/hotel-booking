using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Application.Features.Auth;

public interface IAuthService
{
    Task<Result<TokenResponse, Error>> RegisterAsync(
        string username,
        string email,
        string password,
        string confirmPassword,
        CancellationToken ct);

    Task<Result<TokenResponse, Error>> LoginAsync(
        string username,
        string password,
        string? twoFactorCode,
        CancellationToken ct);

    Task<Result<TokenResponse, Error>> RefreshAsync(
        string refreshToken,
        CancellationToken ct);

    Task<UnitResult<Error>> LogoutAsync(
        string refreshToken,
        CancellationToken ct);
}