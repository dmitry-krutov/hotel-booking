using System;
using SharedKernel;

namespace Shared.Errors;

public static class AuthErrors
{
    public static class Users
    {
        public static Error NotFound(Guid userId) =>
            Error.NotFound(
                "auth.user.not_found",
                $"User '{userId}' was not found");

        public static Error UserNameAlreadyExists(string userName) =>
            Error.AlreadyExist(
                "auth.user.username.already_exists",
                $"Username '{userName}' is already taken");

        public static Error EmailAlreadyExists(string email) =>
            Error.AlreadyExist(
                "auth.user.email.already_exists",
                $"Email '{email}' is already taken");
    }

    public static class Identity
    {
        public static Error PasswordsDoNotMatch() =>
            Error.Validation(
                "auth.password.mismatch",
                "Passwords do not match",
                "confirmPassword");

        public static Error GeneralFailure(string description) =>
            Error.Failure(
                "auth.identity.failure",
                description);
    }

    public static class Credentials
    {
        public static Error Invalid() =>
            Error.Validation(
                "auth.credentials.invalid",
                "Invalid credentials",
                "password");

        public static Error InvalidClient() =>
            Error.NotFound(
                "auth.credentials.invalid_client",
                "User not found");
    }

    public static class TwoFactor
    {
        public static Error Required() =>
            Error.Validation(
                "auth.two_factor.required",
                "Two-factor code is required",
                "twoFactorCode");

        public static Error Invalid() =>
            Error.Validation(
                "auth.two_factor.invalid",
                "Two-factor code is invalid",
                "twoFactorCode");
    }

    public static class Tokens
    {
        public static Error InvalidRefreshToken() =>
            Error.Validation(
                "auth.token.refresh.invalid",
                "Refresh token is invalid",
                "refreshToken");

        public static Error RefreshTokenExpired() =>
            Error.Validation(
                "auth.token.refresh.expired",
                "Refresh token expired",
                "refreshToken");

        public static Error RefreshTokenReuseDetected() =>
            Error.Conflict(
                "auth.token.refresh.reuse_detected",
                "Refresh token is revoked",
                "refreshToken");

        public static Error RefreshTokenCompromised() =>
            Error.Conflict(
                "auth.token.refresh.compromised",
                "Refresh token reuse detected. All sessions revoked.",
                "refreshToken");
    }

    public static class Validation
    {
        public static Error UserIdRequired() =>
            Error.Validation(
                "auth.user.id_required",
                "UserId is required",
                "userId");

        public static Error UserNameRequired() =>
            Error.Validation(
                "auth.user.username_required",
                "UserName is required",
                "userName");

        public static Error UserNameTooLong() =>
            Error.Validation(
                "auth.user.username_too_long",
                "UserName must be at most 50 characters",
                "userName");

        public static Error EmailRequired() =>
            Error.Validation(
                "auth.user.email_required",
                "Email is required",
                "email");

        public static Error EmailInvalid() =>
            Error.Validation(
                "auth.user.email_invalid",
                "Email format is invalid",
                "email");

        public static Error EmailTooLong() =>
            Error.Validation(
                "auth.user.email_too_long",
                "Email must be at most 256 characters",
                "email");

        public static Error PasswordRequired() =>
            Error.Validation(
                "auth.password.required",
                "Password is required",
                "password");

        public static Error PasswordTooShort() =>
            Error.Validation(
                "auth.password.too_short",
                "Password must be at least 6 characters",
                "password");

        public static Error PasswordTooLong() =>
            Error.Validation(
                "auth.password.too_long",
                "Password is too long",
                "password");

        public static Error ConfirmPasswordRequired() =>
            Error.Validation(
                "auth.password.confirmation_required",
                "ConfirmPassword is required",
                "confirmPassword");

        public static Error TwoFactorCodeRequired() =>
            Error.Validation(
                "auth.two_factor.code_required",
                "Two-factor code is required",
                "twoFactorCode");

        public static Error TwoFactorCodeInvalidLength() =>
            Error.Validation(
                "auth.two_factor.code_invalid",
                "Two-factor code must be exactly 6 digits",
                "twoFactorCode");

        public static Error TwoFactorCodeTooLong() =>
            Error.Validation(
                "auth.two_factor.code_too_long",
                "Two-factor code is too long",
                "twoFactorCode");

        public static Error RefreshTokenRequired() =>
            Error.Validation(
                "auth.token.refresh.required",
                "RefreshToken is required",
                "refreshToken");

        public static Error RefreshTokenTooLong() =>
            Error.Validation(
                "auth.token.refresh.too_long",
                "RefreshToken is too long",
                "refreshToken");
    }
}
