using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Booking.ValueObjects;

public sealed class DateRange : ComparableValueObject
{
    private DateRange(DateOnly checkIn, DateOnly checkOut)
    {
        CheckIn = checkIn;
        CheckOut = checkOut;
    }

    public DateOnly CheckIn { get; }

    public DateOnly CheckOut { get; }

    public int Nights =>
        (CheckOut.ToDateTime(TimeOnly.MinValue) - CheckIn.ToDateTime(TimeOnly.MinValue)).Days;

    public static Result<DateRange, Error> Create(
        DateOnly checkIn,
        DateOnly checkOut)
    {
        if (checkOut <= checkIn)
        {
            return GeneralErrors.Validation.InvalidFormat(
                nameof(DateRange),
                "CheckOut must be greater than CheckIn");
        }

        return new DateRange(checkIn, checkOut);
    }

    public bool Overlaps(DateRange other)
    {
        return !(CheckOut <= other.CheckIn || CheckIn >= other.CheckOut);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return CheckIn;
        yield return CheckOut;
    }
}