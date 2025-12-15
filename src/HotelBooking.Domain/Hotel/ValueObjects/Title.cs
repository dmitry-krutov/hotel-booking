using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Hotel.ValueObjects;

public class Title : ComparableValueObject
{
    public const int MIN_LENGTH = 1;
    public const int MAX_LENGTH = 100;

    private Title(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Title, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return GeneralErrors.Validation.ValueIsRequired(nameof(Title));

        var trimmed = value.Trim();

        if (trimmed.Length < MIN_LENGTH)
            return GeneralErrors.Validation.ValueTooSmall(nameof(Title), MIN_LENGTH);

        if (trimmed.Length > MAX_LENGTH)
            return GeneralErrors.Validation.ValueTooLarge(nameof(Title), MAX_LENGTH);

        return new Title(trimmed);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }
}