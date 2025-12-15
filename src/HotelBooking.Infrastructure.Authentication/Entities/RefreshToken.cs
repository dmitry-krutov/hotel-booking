namespace HotelBooking.Infrastructure.Authentication.Entities;

public sealed class RefreshToken
{
    public Guid Id { get; set; }

    public string TokenHash { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime Expires { get; set; }

    public DateTime AbsoluteExpires { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? RevokedReason { get; set; }

    public Guid? ReplacedByTokenId { get; set; }

    public RefreshToken? ReplacedByToken { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;
}