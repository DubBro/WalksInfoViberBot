using Microsoft.EntityFrameworkCore;
using WalksInfoViberBot.Data.Entities;
using WalksInfoViberBot.Data.EntityConfigurations;

namespace WalksInfoViberBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WalkEntity> Walks { get; set; }
        public DbSet<WalksGeneralInfoEntity> WalksGeneralInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new WalkEntityTypeConfiguration());
            builder.ApplyConfiguration(new WalksGeneralInfoEntityTypeConfiguration());
        }
    }
}
