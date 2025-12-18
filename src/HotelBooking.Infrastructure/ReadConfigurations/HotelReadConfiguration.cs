using HotelBooking.Application.Features.Bookings.ReadModels;
using HotelBooking.Domain.Hotel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBooking.Infrastructure.ReadConfigurations;

public class HotelReadConfiguration : IEntityTypeConfiguration<HotelReadModel>
{
    public void Configure(EntityTypeBuilder<HotelReadModel> builder)
    {
        builder.ToTable("hotels");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(Title.MAX_LENGTH);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(Description.MAX_LENGTH);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(Address.COUNTRY_MAX_LENGTH)
            .HasColumnName("country");

        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(Address.CITY_MAX_LENGTH)
            .HasColumnName("city");

        builder.Property(x => x.Region)
            .HasMaxLength(Address.REGION_MAX_LENGTH)
            .HasColumnName("region");

        builder.Property(x => x.Street)
            .IsRequired()
            .HasMaxLength(Address.STREET_MAX_LENGTH)
            .HasColumnName("street");

        builder.Property(x => x.Building)
            .HasMaxLength(Address.BUILDING_MAX_LENGTH)
            .HasColumnName("building");

        builder.Property(x => x.PostalCode)
            .HasMaxLength(Address.POSTAL_CODE_MAX_LENGTH)
            .HasColumnName("postal_code");

        builder.HasMany(x => x.Rooms)
            .WithOne(x => x.Hotel)
            .HasForeignKey(x => x.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
