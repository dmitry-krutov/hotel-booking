using SharedKernel;

namespace HotelBooking.Application.Features.Auth;

public static class AuthErrors
{
    public static Error NotFound(Guid userId)
        => Error.NotFound("user.not.found", $"User {userId} not found");

    public static Error PasswordsDoNotMatch()
        => Error.Validation("password.mismatch", "Passwords do not match");

    public static Error TwoFactorInvalidCode()
        => Error.Validation("invalid.code", "Two-factor code is invalid");

    public static Error IdentityFailure(string description)
        => Error.Validation("identity.failure", description);

    public static Error InvalidClient()
        => Error.Failure("invalid.client", "User not found");

    public static Error InvalidCredentials()
        => Error.Failure("invalid.grant", "Invalid credentials");

    public static Error TwoFactorRequired()
        => Error.Failure("invalid.grant", "Two-factor code required");

    public static Error InvalidTwoFactorCode()
        => Error.Failure("invalid.grant", "Invalid two-factor code");

    public static Error RefreshTokenInvalid()
        => Error.Failure("invalid.grant", "Refresh token is invalid");

    public static Error RefreshTokenReuseDetected()
        => Error.Failure("token.reuse.detected", "Refresh token is revoked");

    public static Error RefreshTokenExpired()
        => Error.Failure("token.expired", "Refresh token expired");

    public static Error RefreshTokenCompromised()
        => Error.Failure("token.compromised", "Refresh token reuse detected. All sessions revoked.");
}