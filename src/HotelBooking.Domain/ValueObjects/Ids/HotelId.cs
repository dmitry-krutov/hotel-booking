using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.ValueObjects.Ids;

public class HotelId : ComparableValueObject
{
    private HotelId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static HotelId NewId() => new(Guid.NewGuid());

    public static HotelId Empty() => new(Guid.Empty);

    public static HotelId Create(Guid id) => new(id);

    public static Result<HotelId, Error> TryCreate(Guid? id)
    {
        if (!id.HasValue)
            return GeneralErrors.Validation.ValueIsRequired(nameof(HotelId));

        if (id.Value == Guid.Empty)
            return GeneralErrors.Validation.ValueIsRequired(nameof(HotelId));

        return Result.Success<HotelId, Error>(Create(id.Value));
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}