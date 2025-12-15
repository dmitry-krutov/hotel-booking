namespace Core;

public interface ICurrentUser
{
    Guid UserId { get; }

    bool IsAuthenticated { get; }
}