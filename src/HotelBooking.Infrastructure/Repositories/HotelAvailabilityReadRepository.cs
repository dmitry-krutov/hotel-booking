using Core.Database;
using Dapper;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Application.Features.Hotels.Queries.GetHotelAvailability;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Domain.Booking.Enums;

namespace HotelBooking.Infrastructure.Repositories;

public class HotelAvailabilityReadRepository : IHotelAvailabilityReadRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public HotelAvailabilityReadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<HotelReadModel?> GetHotelWithAvailableRooms(
        GetHotelAvailabilityQuery query,
        CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT id,
                                  title,
                                  country,
                                  city,
                                  region,
                                  street,
                                  building,
                                  postal_code,
                                  description
                           FROM hotels
                           WHERE id = @HotelId;

                           SELECT id,
                                  hotel_id,
                                  title,
                                  price_per_night,
                                  capacity,
                                  is_active
                           FROM room
                           WHERE hotel_id = @HotelId
                             AND is_active = TRUE
                             AND capacity >= @Guests
                             AND NOT EXISTS (
                                 SELECT 1
                                 FROM bookings AS b
                                 WHERE b.room_id = room.id
                                   AND b.status = @ActiveStatus
                                   AND b.check_in < @CheckOut
                                   AND b.check_out > @CheckIn
                             );
                           """;

        var parameters = new
        {
            query.HotelId,
            query.Guests,
            CheckIn = query.CheckIn.ToDateTime(TimeOnly.MinValue),
            CheckOut = query.CheckOut.ToDateTime(TimeOnly.MinValue),
            ActiveStatus = (int)BookingStatus.ACTIVE
        };

        await using var connection = await _connectionFactory.CreateOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

        using var grid = await connection.QueryMultipleAsync(command);

        var hotel = await grid.ReadSingleOrDefaultAsync<HotelReadModel>();
        if (hotel is null)
            return null;

        var rooms = (await grid.ReadAsync<RoomReadModel>()).ToList();
        hotel.Rooms = rooms;

        return hotel;
    }
}