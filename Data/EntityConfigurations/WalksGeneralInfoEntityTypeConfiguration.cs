using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalksInfoViberBot.Data.Entities;

namespace WalksInfoViberBot.Data.EntityConfigurations
{
    public class WalksGeneralInfoEntityTypeConfiguration : IEntityTypeConfiguration<WalksGeneralInfoEntity>
    {
        public void Configure(EntityTypeBuilder<WalksGeneralInfoEntity> builder)
        {
            builder.HasNoKey();
            builder.ToView("SelectWalksGeneralInfo");

            builder.Property(wgi => wgi.TotalCount)
                .HasColumnName("CountOfWalks")
                .HasColumnType("int");

            builder.Property(wgi => wgi.TotalDistance)
                .HasColumnName("TotalDistanceInKm")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            builder.Property(wgi => wgi.TotalDuration)
                .HasColumnName("TotalDurationInMin")
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
        }
    }
}
