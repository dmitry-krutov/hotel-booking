using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Hotel.ValueObjects;

public sealed class Capacity : ComparableValueObject
{
    public const int MIN_VALUE = 1;
    public const int MAX_VALUE = 20;

    private Capacity(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static Result<Capacity, Error> Create(int value)
    {
        if (value < MIN_VALUE)
            return GeneralErrors.Validation.ValueTooSmall(nameof(Capacity), MIN_VALUE);

        if (value > MAX_VALUE)
            return GeneralErrors.Validation.ValueTooLarge(nameof(Capacity), MAX_VALUE);

        return new Capacity(value);
    }

    public override string ToString() => Value.ToString();

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}