using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HotelBooking.Infrastructure.Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelBooking.Infrastructure.Authentication.Services;

public class TokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AuthorizationDbContext _context;
    private readonly JwtSettings _settings;

    public TokenService(
        UserManager<ApplicationUser> userManager,
        AuthorizationDbContext context,
        IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _context = context;
        _settings = options.Value;
    }

    public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
        };
        claims.AddRange(userClaims);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(
        ApplicationUser user,
        RefreshToken? previous = null,
        CancellationToken ct = default)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refresh = new RefreshToken
        {
            Id = Guid.NewGuid(),
            TokenHash = ComputeRefreshTokenHash(token),
            Created = previous?.Created ?? DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays),
            AbsoluteExpires = previous?.AbsoluteExpires
                              ?? DateTime.UtcNow.AddDays(_settings.RefreshTokenAbsoluteExpirationDays),
            UserId = user.Id
        };

        _context.RefreshTokens.Add(refresh);

        if (previous is not null)
        {
            previous.ReplacedByTokenId = refresh.Id;
        }

        await _context.SaveChangesAsync(ct);
        return token;
    }

    public string HashRefreshToken(string refreshToken) => ComputeRefreshTokenHash(refreshToken);

    private string ComputeRefreshTokenHash(string token)
    {
        if (string.IsNullOrWhiteSpace(_settings.RefreshHashKey))
            throw new InvalidOperationException("JwtSettings.RefreshHashKey is not configured.");

        var keyBytes = Encoding.UTF8.GetBytes(_settings.RefreshHashKey);
        var tokenBytes = Encoding.UTF8.GetBytes(token);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(tokenBytes);

        return Convert.ToBase64String(hashBytes);
    }
}