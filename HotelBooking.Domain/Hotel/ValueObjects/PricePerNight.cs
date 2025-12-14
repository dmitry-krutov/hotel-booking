using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Hotel.ValueObjects;

public class PricePerNight : ComparableValueObject
{
    public const decimal MIN_VALUE = 0.01m;
    public const decimal MAX_VALUE = 1_000_000m;

    private PricePerNight(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Result<PricePerNight, Error> Create(decimal value)
    {
        if (value < MIN_VALUE)
            return GeneralErrors.Validation.ValueTooSmall(nameof(PricePerNight), MIN_VALUE);

        if (value > MAX_VALUE)
            return GeneralErrors.Validation.ValueTooLarge(nameof(PricePerNight), MAX_VALUE);

        var normalized = decimal.Round(value, 2, MidpointRounding.AwayFromZero);

        return new PricePerNight(normalized);
    }

    public override string ToString() => Value.ToString("0.00");

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}