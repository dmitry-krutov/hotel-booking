namespace HotelBooking.Application.Features.Auth;

public interface IAuthorizationService
{
    Task<bool> IsAdminAsync(Guid userId, CancellationToken ct);
}
