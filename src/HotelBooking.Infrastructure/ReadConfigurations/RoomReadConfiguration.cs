using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Domain.Hotel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.ReadConfigurations;

public class RoomReadConfiguration : IEntityTypeConfiguration<RoomReadModel>
{
    public void Configure(EntityTypeBuilder<RoomReadModel> builder)
    {
        builder.ToTable("room");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.HotelId)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Title.MAX_LENGTH);

        builder.Property(x => x.PricePerNight)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.Capacity)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.Hotel)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
