using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Booking.ValueObjects;

public sealed class GuestsCount : ComparableValueObject
{
    public const int MIN = 1;
    public const int MAX = 20;

    private GuestsCount(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<GuestsCount, Error> Create(int value)
    {
        if (value < MIN)
            return GeneralErrors.Validation.ValueTooSmall(nameof(GuestsCount), MIN);

        if (value > MAX)
            return GeneralErrors.Validation.ValueTooLarge(nameof(GuestsCount), MAX);

        return new GuestsCount(value);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}