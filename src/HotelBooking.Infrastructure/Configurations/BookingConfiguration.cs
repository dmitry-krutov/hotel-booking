using HotelBooking.Domain.Booking;
using HotelBooking.Domain.Booking.ValueObjects;
using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => BookingId.Create(value));

        builder.Property(x => x.HotelId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => HotelId.Create(value));

        builder.Property(x => x.RoomId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => RoomId.Create(value));

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.Guests)
            .IsRequired()
            .HasConversion(
                vo => vo.Value,
                value => GuestsCount.Create(value).Value);

        builder.OwnsOne(x => x.Period, period =>
        {
            period.WithOwner();

            period.Property(x => x.CheckIn)
                .IsRequired()
                .HasColumnName("check_in")
                .HasConversion(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    dt => DateOnly.FromDateTime(dt));

            period.Property(x => x.CheckOut)
                .IsRequired()
                .HasColumnName("check_out")
                .HasConversion(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    dt => DateOnly.FromDateTime(dt));

            period.HasIndex(x => new { x.CheckIn, x.CheckOut });
        });

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.HotelId);

        builder.HasOne<Hotel>()
            .WithMany()
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Room>()
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}