using HotelBooking.Application.Features.Bookings.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.ReadConfigurations;

public class BookingReadConfiguration : IEntityTypeConfiguration<BookingReadModel>
{
    public void Configure(EntityTypeBuilder<BookingReadModel> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.HotelId)
            .IsRequired();

        builder.Property(x => x.RoomId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CheckIn)
            .IsRequired()
            .HasColumnName("check_in")
            .HasConversion(
                d => d.ToDateTime(TimeOnly.MinValue),
                dt => DateOnly.FromDateTime(dt));

        builder.Property(x => x.CheckOut)
            .IsRequired()
            .HasColumnName("check_out")
            .HasConversion(
                d => d.ToDateTime(TimeOnly.MinValue),
                dt => DateOnly.FromDateTime(dt));

        builder.Property(x => x.Guests)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasColumnName("total_price");

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired()
            .HasColumnName("created_at_utc");

        builder.HasOne(x => x.Hotel)
            .WithMany()
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Room)
            .WithMany()
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.HotelId);
    }
}
