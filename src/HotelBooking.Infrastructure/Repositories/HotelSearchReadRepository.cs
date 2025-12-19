using Core.Database;
using Dapper;
using HotelBooking.Application.Features.Hotels;
using HotelBooking.Application.Features.Hotels.Queries.SearchHotels;
using HotelBooking.Application.Features.Hotels.ReadModels;
using HotelBooking.Contracts.Hotels;
using HotelBooking.Domain.Booking.Enums;
using SharedKernel;

namespace HotelBooking.Infrastructure.Repositories;

public class HotelSearchReadRepository : IHotelSearchReadRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public HotelSearchReadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PagedList<HotelSearchResultReadModel>> SearchHotels(
        SearchHotelsQuery query,
        CancellationToken cancellationToken)
    {
        const string baseSql = """
                               WITH filtered_rooms AS (
                                   SELECT r.hotel_id, r.price_per_night
                                   FROM room AS r
                                   WHERE r.is_active = TRUE
                                     AND r.capacity >= @Guests
                                     AND (@MinPrice IS NULL OR r.price_per_night >= @MinPrice)
                                     AND (@MaxPrice IS NULL OR r.price_per_night <= @MaxPrice)
                                     AND NOT EXISTS (
                                         SELECT 1
                                         FROM bookings AS b
                                         WHERE b.room_id = r.id
                                           AND b.status = @ActiveStatus
                                           AND b.check_in < @CheckOut
                                           AND b.check_out > @CheckIn
                                     )
                               ),
                               hotel_candidates AS (
                                   SELECT h.id AS HotelId,
                                          h.title AS Title,
                                          h.city AS City,
                                          h.country AS Country,
                                          MIN(fr.price_per_night) AS MinPricePerNight,
                                          COUNT(*) AS AvailableRoomsCount
                                   FROM hotels AS h
                                   INNER JOIN filtered_rooms AS fr ON fr.hotel_id = h.id
                                   WHERE (@City IS NULL OR h.city = @City)
                                     AND (@Country IS NULL OR h.country = @Country)
                                     AND (@Region IS NULL OR h.region = @Region)
                                   GROUP BY h.id, h.title, h.city, h.country
                               )
                               SELECT
                                   HotelId,
                                   Title,
                                   City,
                                   Country,
                                   MinPricePerNight,
                                   AvailableRoomsCount,
                                   COUNT(*) OVER() AS TotalCount
                               FROM hotel_candidates
                               ORDER BY {0}
                               LIMIT @PageSize OFFSET @Offset;
                               """;

        var parameters = new
        {
            query.Guests,
            query.MinPrice,
            query.MaxPrice,
            City = Normalize(query.City),
            Country = Normalize(query.Country),
            Region = Normalize(query.Region),
            CheckIn = query.CheckIn.ToDateTime(TimeOnly.MinValue),
            CheckOut = query.CheckOut.ToDateTime(TimeOnly.MinValue),
            query.PageSize,
            Offset = (query.PageNumber - 1) * query.PageSize,
            ActiveStatus = (int)BookingStatus.ACTIVE
        };

        var sortClause = BuildSortClause(query.Sort);
        var sql = string.Format(baseSql, sortClause);

        await using var connection = await _connectionFactory.CreateOpenConnectionAsync(cancellationToken);

        var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

        var rows = (await connection.QueryAsync<HotelSearchRow>(command)).ToList();

        var totalCount = rows.Count == 0 ? 0 : rows[0].TotalCount;
        var hotels = rows.Select(x => new HotelSearchResultReadModel
        {
            HotelId = x.HotelId,
            Title = x.Title,
            City = x.City,
            Country = x.Country,
            MinPricePerNight = x.MinPricePerNight,
            AvailableRoomsCount = x.AvailableRoomsCount
        }).ToList();

        return new PagedList<HotelSearchResultReadModel>(hotels, totalCount, query.PageNumber, query.PageSize);
    }

    private sealed class HotelSearchRow
    {
        public Guid HotelId { get; init; }

        public string Title { get; init; } = null!;

        public string City { get; init; } = null!;

        public string Country { get; init; } = null!;

        public decimal MinPricePerNight { get; init; }

        public int AvailableRoomsCount { get; init; }

        public int TotalCount { get; init; }
    }

    private static string BuildSortClause(HotelSearchSortOption sort)
    {
        return sort switch
        {
            HotelSearchSortOption.PriceDesc => "MinPricePerNight DESC, Title, HotelId",
            _ => "MinPricePerNight ASC, Title, HotelId"
        };
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}