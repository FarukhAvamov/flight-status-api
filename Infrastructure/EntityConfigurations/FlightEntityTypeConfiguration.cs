using FlighStatusApi.Domain.AggregateModels.FlightAggregate.cs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityConfigurations
{
    internal class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
    {
      

        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            var statusConverter = new ValueConverter<Status, string>(
                v => v.ToString(),
                v => (Status)Enum.Parse(typeof(Status),v)
            );
            
            builder.ToTable("Flights");

            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd();

            builder.Property(p => p.Origin).HasColumnType("nvarchar(256)");

            builder.Property(p => p.Destination).HasColumnType("nvarchar(256)");
            
            builder.Property(p => p.Status).HasConversion(statusConverter)
                .HasColumnName("Status")
                .HasColumnType("nvarchar(256)")
                .IsRequired();
        }
    }
}
