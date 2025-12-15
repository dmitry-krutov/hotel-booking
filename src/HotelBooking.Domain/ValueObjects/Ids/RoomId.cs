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

    public static Result<RoomId, Error> TryCreate(Guid? id)
    {
        if (!id.HasValue)
            return GeneralErrors.Validation.ValueIsRequired(nameof(RoomId));

        if (id.Value == Guid.Empty)
            return GeneralErrors.Validation.ValueIsRequired(nameof(RoomId));

        return Result.Success<RoomId, Error>(Create(id.Value));
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}