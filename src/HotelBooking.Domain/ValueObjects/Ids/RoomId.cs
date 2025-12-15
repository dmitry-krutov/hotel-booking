using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.ValueObjects.Ids;

public class RoomId : ComparableValueObject
{
    private RoomId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static RoomId NewId() => new(Guid.NewGuid());

    public static RoomId Empty() => new(Guid.Empty);

    public static RoomId Create(Guid id) => new(id);

    public static Result<RoomId, Error> TryCreate(Guid id) => Create(id);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}