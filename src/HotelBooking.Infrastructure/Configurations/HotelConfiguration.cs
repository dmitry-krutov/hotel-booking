using HotelBooking.Domain.Hotel;
using HotelBooking.Domain.Hotel.ValueObjects;
using HotelBooking.Domain.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HotelId.Create(value));

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Title.MAX_LENGTH)
            .HasConversion(
                vo => vo.Value,
                value => Title.Create(value).Value);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(Description.MAX_LENGTH)
            .HasConversion(
                vo => vo.Value,
                value => Description.Create(value).Value);

        builder.OwnsOne(x => x.Address, address =>
        {
            address.WithOwner();

            address.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(Address.COUNTRY_MAX_LENGTH)
                .HasColumnName("Country");

            address.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(Address.CITY_MAX_LENGTH)
                .HasColumnName("City");

            address.Property(x => x.Region)
                .HasMaxLength(Address.REGION_MAX_LENGTH)
                .HasColumnName("Region");

            address.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(Address.STREET_MAX_LENGTH)
                .HasColumnName("Street");

            address.Property(x => x.Building)
                .HasMaxLength(Address.BUILDING_MAX_LENGTH)
                .HasColumnName("Building");

            address.Property(x => x.PostalCode)
                .HasMaxLength(Address.POSTAL_CODE_MAX_LENGTH)
                .HasColumnName("PostalCode");

            address.HasIndex(x => x.City);
        });

        builder.Metadata
            .FindNavigation(nameof(Hotel.Rooms))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany<Room>("_rooms")
            .WithOne()
            .HasForeignKey(x => x.HotelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}