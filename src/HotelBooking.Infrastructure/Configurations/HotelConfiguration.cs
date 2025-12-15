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
                .HasColumnName("country");

            address.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(Address.CITY_MAX_LENGTH)
                .HasColumnName("city");

            address.Property(x => x.Region)
                .HasMaxLength(Address.REGION_MAX_LENGTH)
                .HasColumnName("region");

            address.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(Address.STREET_MAX_LENGTH)
                .HasColumnName("street");

            address.Property(x => x.Building)
                .HasMaxLength(Address.BUILDING_MAX_LENGTH)
                .HasColumnName("building");

            address.Property(x => x.PostalCode)
                .HasMaxLength(Address.POSTAL_CODE_MAX_LENGTH)
                .HasColumnName("postal_code");

            address.HasIndex(x => x.City);
        });

        builder.Metadata
            .FindNavigation(nameof(Hotel.Rooms))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Rooms)
            .WithOne()
            .HasForeignKey(x => x.HotelId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}