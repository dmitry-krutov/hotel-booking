using SharedKernel;

namespace HotelBooking.Application.Features.Bookings;

public static class BookingErrors
{
    public static Error RoomNotFound(Guid roomId) =>
        GeneralErrors.Entity.NotFound("Room", roomId);

    public static Error RoomInactive(Guid roomId) =>
        GeneralErrors.Entity.InvalidState("Room", $"Room {roomId} is not active");

    public static Error GuestsExceedCapacity(int capacity) =>
        Error.Validation(
            "booking.guests.exceed.capacity",
            $"Guests count exceeds room capacity ({capacity})");

    public static Error RoomUnavailable(DateOnly checkIn, DateOnly checkOut) =>
        Error.Conflict(
            "booking.room.unavailable",
            $"Room is not available for period {checkIn:yyyy-MM-dd} - {checkOut:yyyy-MM-dd}");

    public static Error CheckInInPast() =>
        Error.Validation(
            "booking.checkin.past",
            "Check-in date must not be in the past");
}
