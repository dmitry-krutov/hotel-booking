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
        public static Error HotelIdRequired() =>
            Error.Validation(
                "hotels.availability.hotel_id_required",
                "HotelId is required",
                "hotelId");

        public static Error GuestsMustBePositive() =>
            Error.Validation(
                "hotels.search.guests_positive",
                "Guests must be greater than zero",
                "guests");

        public static Error CheckInRequired() =>
            Error.Validation(
                "hotels.search.check_in_required",
                "Check-in date is required",
                "checkIn");

        public static Error CheckOutRequired() =>
            Error.Validation(
                "hotels.search.check_out_required",
                "Check-out date is required",
                "checkOut");

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

        public static Error PageNumberPositive() =>
            Error.Validation(
                "hotels.search.page_number_positive",
                "PageNumber must be greater than zero",
                "pageNumber");

        public static Error PageSizePositive() =>
            Error.Validation(
                "hotels.search.page_size_positive",
                "PageSize must be greater than zero",
                "pageSize");

        public static Error PageSizeTooLarge(int max) =>
            Error.Validation(
                "hotels.search.page_size_too_large",
                $"PageSize must not exceed {max}",
                "pageSize");

        public static Error MinPriceNegative() =>
            Error.Validation(
                "hotels.search.min_price_negative",
                "MinPrice must be greater than or equal to zero",
                "minPrice");

        public static Error MaxPriceNegative() =>
            Error.Validation(
                "hotels.search.max_price_negative",
                "MaxPrice must be greater than or equal to zero",
                "maxPrice");
    }
}
