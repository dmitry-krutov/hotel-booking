using CSharpFunctionalExtensions;
using HotelBooking.Application.Features.Auth;
using HotelBooking.Infrastructure.Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace HotelBooking.Infrastructure.Authentication.Services;

public class TwoFactorService : ITwoFactorService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public TwoFactorService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string, ErrorList>> GenerateTwoFactorSecretAsync(Guid userId, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return AuthErrors.NotFound(userId).ToErrorList();

        await _userManager.ResetAuthenticatorKeyAsync(user);
        var key = await _userManager.GetAuthenticatorKeyAsync(user);
        return key!;
    }

    public async Task<Result<Guid, ErrorList>> EnableTwoFactorAsync(Guid userId, string code, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return AuthErrors.NotFound(userId).ToErrorList();

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(
            user,
            _userManager.Options.Tokens.AuthenticatorTokenProvider,
            code);
        if (!isValid)
            return AuthErrors.TwoFactorInvalidCode().ToErrorList();

        await _userManager.SetTwoFactorEnabledAsync(user, true);
        return user.Id;
    }

    public async Task<Result<Guid, ErrorList>> DisableTwoFactorAsync(Guid userId, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return AuthErrors.NotFound(userId).ToErrorList();

        await _userManager.SetTwoFactorEnabledAsync(user, false);
        await _userManager.ResetAuthenticatorKeyAsync(user);
        return user.Id;
    }
}