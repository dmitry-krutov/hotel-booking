using System.Security.Claims;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Framework;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public Guid UserId
    {
        get
        {
            if (!IsAuthenticated)
                throw new InvalidOperationException("User is not authenticated");

            var user = _httpContextAccessor.HttpContext!.User;

            var userIdClaim =
                user.FindFirstValue(ClaimTypes.NameIdentifier) ??
                user.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new InvalidOperationException("UserId claim is missing or invalid");

            return userId;
        }
    }
}