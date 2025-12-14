using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Hotel.ValueObjects;

public class Description : ComparableValueObject
{
    public const int MIN_LENGTH = 1;
    public const int MAX_LENGTH = 2000;

    private Description(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Description, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.Validation.ValueIsRequired(nameof(Description));

        var trimmed = value.Trim();

        if (trimmed.Length < MIN_LENGTH)
            return GeneralErrors.Validation.ValueTooSmall(nameof(Description), MIN_LENGTH);

        if (trimmed.Length > MAX_LENGTH)
            return GeneralErrors.Validation.ValueTooLarge(nameof(Description), MAX_LENGTH);

        return new Description(trimmed);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}