using System;
using SharedKernel;

namespace Shared.Errors;

public static class BookingErrors
{
    public static class Bookings
    {
        public static Error NotFound(Guid bookingId) =>
            Error.NotFound(
                "booking.booking.not_found",
                $"Booking '{bookingId}' was not found");

        public static Error AccessDenied() =>
            Error.Failure(
                "booking.booking.access_denied",
                "You are not allowed to view this booking");
    }

    public static class Rooms
    {
        public static Error NotFound(Guid roomId) =>
            Error.NotFound(
                "booking.room.not_found",
                $"Room '{roomId}' was not found");

        public static Error Inactive(Guid roomId) =>
            Error.Conflict(
                "booking.room.inactive",
                $"Room '{roomId}' is not active");

        public static Error Unavailable(DateOnly checkIn, DateOnly checkOut) =>
            Error.Conflict(
                "booking.room.unavailable",
                $"Room is not available for period {checkIn:yyyy-MM-dd} - {checkOut:yyyy-MM-dd}");
    }

    public static class Validation
    {
        public static Error GuestsExceedCapacity(int capacity) =>
            Error.Validation(
                "booking.guests.exceed_capacity",
                $"Guests count exceeds room capacity ({capacity})",
                "guests");

        public static Error CheckInInPast() =>
            Error.Validation(
                "booking.dates.check_in_in_past",
                "Check-in date must not be in the past",
                "checkIn");

        public static Error InvalidDateRange() =>
            Error.Validation(
                "booking.dates.invalid_range",
                "Check-out date must be later than check-in date",
                "checkOut");
    }
}
