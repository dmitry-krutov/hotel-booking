using CSharpFunctionalExtensions;
using HotelBooking.Application.Features.Auth;
using HotelBooking.Infrastructure.Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace HotelBooking.Infrastructure.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly TokenService _tokenService;
    private readonly AuthorizationDbContext _context;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        TokenService tokenService,
        AuthorizationDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<Result<TokenResponse, Error>> RegisterAsync(
        string username,
        string email,
        string password,
        string confirmPassword,
        CancellationToken ct)
    {
        if (password != confirmPassword)
            return AuthErrors.PasswordsDoNotMatch();

        var existingByName = await _userManager.FindByNameAsync(username);
        if (existingByName != null)
            return AuthErrors.IdentityFailure("Username is already taken.");

        var existingByEmail = await _userManager.FindByEmailAsync(email);
        if (existingByEmail != null)
            return AuthErrors.IdentityFailure("Email is already taken.");

        var user = new ApplicationUser { UserName = username, Email = email };

        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return AuthErrors.IdentityFailure(string.Join("; ", createResult.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.USER);
        if (!roleResult.Succeeded)
            return AuthErrors.IdentityFailure(string.Join("; ", roleResult.Errors.Select(e => e.Description)));

        var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user, previous: null, ct);

        return new TokenResponse(accessToken, refreshToken);
    }

    public async Task<Result<TokenResponse, Error>> LoginAsync(
        string username,
        string password,
        string? twoFactorCode,
        CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            return AuthErrors.InvalidClient();

        if (!await _userManager.CheckPasswordAsync(user, password))
            return AuthErrors.InvalidCredentials();

        if (await _userManager.GetTwoFactorEnabledAsync(user))
        {
            if (string.IsNullOrWhiteSpace(twoFactorCode))
                return AuthErrors.TwoFactorRequired();

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user,
                _userManager.Options.Tokens.AuthenticatorTokenProvider,
                twoFactorCode);

            if (!isValid)
                return AuthErrors.InvalidTwoFactorCode();
        }

        var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user, previous: null, ct);

        return new TokenResponse(accessToken, refreshToken);
    }

    public async Task<Result<TokenResponse, Error>> RefreshAsync(string refreshToken, CancellationToken ct)
    {
        var hash = _tokenService.HashRefreshToken(refreshToken);

        var refresh = await _context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.TokenHash == hash, ct);

        if (refresh == null)
            return AuthErrors.RefreshTokenInvalid();

        var now = DateTime.UtcNow;

        if (refresh.IsRevoked)
        {
            if (refresh.RevokedReason == RefreshRevokeReasons.ROTATED)
            {
                await RevokeAllUserRefreshTokensAsync(
                    refresh.UserId,
                    RefreshRevokeReasons.REUSE_DETECTED,
                    now,
                    ct);

                return AuthErrors.RefreshTokenReuseDetected();
            }

            return AuthErrors.RefreshTokenInvalid();
        }

        if (refresh.Expires < now || refresh.AbsoluteExpires < now)
            return AuthErrors.RefreshTokenExpired();

        refresh.IsRevoked = true;
        refresh.RevokedAt = now;
        refresh.RevokedReason = RefreshRevokeReasons.ROTATED;

        var accessToken = await _tokenService.GenerateAccessTokenAsync(refresh.User);
        var newRefresh = await _tokenService.GenerateRefreshTokenAsync(refresh.User, refresh, ct);

        return new TokenResponse(accessToken, newRefresh);
    }

    public async Task<UnitResult<Error>> LogoutAsync(string refreshToken, CancellationToken ct)
    {
        var hash = _tokenService.HashRefreshToken(refreshToken);

        var refresh = await _context.RefreshTokens
            .FirstOrDefaultAsync(r => r.TokenHash == hash, ct);

        if (refresh == null)
            return AuthErrors.RefreshTokenInvalid();

        if (refresh.IsRevoked)
            return Result.Success<Error>();

        refresh.IsRevoked = true;
        refresh.RevokedAt = DateTime.UtcNow;
        refresh.RevokedReason = RefreshRevokeReasons.LOGOUT;

        await _context.SaveChangesAsync(ct);
        return Result.Success<Error>();
    }

    private async Task RevokeAllUserRefreshTokensAsync(
        Guid userId,
        string reason,
        DateTime now,
        CancellationToken ct)
    {
        var tokens = await _context.RefreshTokens
            .Where(t => t.UserId == userId && !t.IsRevoked)
            .ToListAsync(ct);

        if (tokens.Count == 0)
            return;

        foreach (var t in tokens)
        {
            t.IsRevoked = true;
            t.RevokedAt = now;
            t.RevokedReason = reason;
        }

        await _context.SaveChangesAsync(ct);
    }
}