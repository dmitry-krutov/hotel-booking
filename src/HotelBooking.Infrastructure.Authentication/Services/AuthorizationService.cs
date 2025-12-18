using HotelBooking.Application.Features.Auth;
using HotelBooking.Infrastructure.Authentication.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Infrastructure.Authentication.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthorizationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> IsAdminAsync(Guid userId, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return false;

        return await _userManager.IsInRoleAsync(user, RoleNames.ADMIN);
    }
}
