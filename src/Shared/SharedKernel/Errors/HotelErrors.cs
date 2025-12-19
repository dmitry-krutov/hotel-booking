using System;
using SharedKernel;

namespace Shared.Errors;

public static class HotelErrors
{
    public static class Hotels
    {
        public static Error NotFound(Guid hotelId) =>
            Error.NotFound(
                "hotels.hotel.not_found",
                $"Hotel '{hotelId}' was not found");
    }

    public static class Rooms
    {
        public static Error NotFound(Guid roomId) =>
            Error.NotFound(
                "hotels.room.not_found",
                $"Room '{roomId}' was not found");
    }

    public static class Validation
    {
        public static Error InvalidPriceRange() =>
            Error.Validation(
                "hotels.search.invalid_price_range",
                "MinPrice must be less than or equal to MaxPrice.",
                "minPrice");

        public static Error InvalidAvailabilityDateRange() =>
            Error.Validation(
                "hotels.availability.invalid_date_range",
                "Check-in date must be earlier than check-out date.",
                "checkIn");
    }
}
