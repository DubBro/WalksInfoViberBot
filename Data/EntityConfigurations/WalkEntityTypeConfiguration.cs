using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalksInfoViberBot.Data.Entities;

namespace WalksInfoViberBot.Data.EntityConfigurations
{
    public class WalkEntityTypeConfiguration : IEntityTypeConfiguration<WalkEntity>
    {
        public void Configure(EntityTypeBuilder<WalkEntity> builder)
        {
            builder.HasNoKey();
            builder.ToView("Select10LongestWalks");

            builder.Property(w => w.Distance)
                .HasColumnName("DistanceInKm")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            builder.Property(w => w.Duration)
                .HasColumnName("DurationInMin")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
        }
    }
}
