using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.ValueObjects.Ids;

public class BookingId : ComparableValueObject
{
    private BookingId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static BookingId NewId() => new(Guid.NewGuid());

    public static BookingId Empty() => new(Guid.Empty);

    public static BookingId Create(Guid id) => new(id);

    public static Result<BookingId, Error> TryCreate(Guid id) => Create(id);

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}