using CSharpFunctionalExtensions;
using SharedKernel;

namespace HotelBooking.Domain.Hotel.ValueObjects;

public sealed class Address : ComparableValueObject
{
    public const int COUNTRY_MIN_LENGTH = 2;
    public const int COUNTRY_MAX_LENGTH = 100;

    public const int CITY_MIN_LENGTH = 1;
    public const int CITY_MAX_LENGTH = 100;

    public const int REGION_MAX_LENGTH = 100;

    public const int STREET_MIN_LENGTH = 1;
    public const int STREET_MAX_LENGTH = 200;

    public const int BUILDING_MAX_LENGTH = 20;

    public const int POSTAL_CODE_MAX_LENGTH = 20;

    private Address(
        string country,
        string city,
        string? region,
        string street,
        string? building,
        string? postalCode)
    {
        Country = country;
        City = city;
        Region = region;
        Street = street;
        Building = building;
        PostalCode = postalCode;
    }

    public string Country { get; }

    public string City { get; }

    public string? Region { get; }

    public string Street { get; }

    public string? Building { get; }

    public string? PostalCode { get; }

    public static Result<Address, Error> Create(
        string country,
        string city,
        string street,
        string? region = null,
        string? building = null,
        string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(country))
            return GeneralErrors.Validation.ValueIsRequired(nameof(Country));

        if (string.IsNullOrWhiteSpace(city))
            return GeneralErrors.Validation.ValueIsRequired(nameof(City));

        if (string.IsNullOrWhiteSpace(street))
            return GeneralErrors.Validation.ValueIsRequired(nameof(Street));

        var normalizedCountry = country.Trim();
        var normalizedCity = city.Trim();
        var normalizedStreet = street.Trim();

        if (normalizedCountry.Length < COUNTRY_MIN_LENGTH)
            return GeneralErrors.Validation.ValueTooSmall(nameof(Country), COUNTRY_MIN_LENGTH);

        if (normalizedCountry.Length > COUNTRY_MAX_LENGTH)
            return GeneralErrors.Validation.ValueTooLarge(nameof(Country), COUNTRY_MAX_LENGTH);

        if (normalizedCity.Length < CITY_MIN_LENGTH)
            return GeneralErrors.Validation.ValueTooSmall(nameof(City), CITY_MIN_LENGTH);

        if (normalizedCity.Length > CITY_MAX_LENGTH)
            return GeneralErrors.Validation.ValueTooLarge(nameof(City), CITY_MAX_LENGTH);

        if (normalizedStreet.Length < STREET_MIN_LENGTH)
            return GeneralErrors.Validation.ValueTooSmall(nameof(Street), STREET_MIN_LENGTH);

        if (normalizedStreet.Length > STREET_MAX_LENGTH)
            return GeneralErrors.Validation.ValueTooLarge(nameof(Street), STREET_MAX_LENGTH);

        string? normalizedRegion = null;
        if (!string.IsNullOrWhiteSpace(region))
        {
            normalizedRegion = region.Trim();
            if (normalizedRegion.Length > REGION_MAX_LENGTH)
                return GeneralErrors.Validation.ValueTooLarge(nameof(Region), REGION_MAX_LENGTH);
        }

        string? normalizedBuilding = null;
        if (!string.IsNullOrWhiteSpace(building))
        {
            normalizedBuilding = building.Trim();
            if (normalizedBuilding.Length > BUILDING_MAX_LENGTH)
                return GeneralErrors.Validation.ValueTooLarge(nameof(Building), BUILDING_MAX_LENGTH);
        }

        string? normalizedPostalCode = null;
        if (!string.IsNullOrWhiteSpace(postalCode))
        {
            normalizedPostalCode = postalCode.Trim();
            if (normalizedPostalCode.Length > POSTAL_CODE_MAX_LENGTH)
                return GeneralErrors.Validation.ValueTooLarge(nameof(PostalCode), POSTAL_CODE_MAX_LENGTH);
        }

        return new Address(
            normalizedCountry,
            normalizedCity,
            normalizedRegion,
            normalizedStreet,
            normalizedBuilding,
            normalizedPostalCode);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Country;
        yield return City;
        yield return Region ?? string.Empty;
        yield return Street;
        yield return Building ?? string.Empty;
        yield return PostalCode ?? string.Empty;
    }
}