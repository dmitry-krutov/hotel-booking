using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => RoomId.Create(value));

        builder.Property(x => x.HotelId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => HotelId.Create(value));

        builder.HasIndex(x => x.HotelId);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Title.MAX_LENGTH)
            .HasConversion(
                vo => vo.Value,
                value => Title.Create(value).Value);

        builder.Property(x => x.PricePerNight)
            .IsRequired()
            .HasPrecision(10, 2)
            .HasConversion(
                vo => vo.Value,
                value => PricePerNight.Create(value).Value);

        builder.Property(x => x.Capacity)
            .IsRequired()
            .HasConversion(
                vo => vo.Value,
                value => Capacity.Create(value).Value);

        builder.Property(x => x.IsActive)
            .IsRequired();
    }
}