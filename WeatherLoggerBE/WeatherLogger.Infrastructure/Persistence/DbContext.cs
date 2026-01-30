using Microsoft.EntityFrameworkCore;
using WeatherLogger.Domain.Entities.Log;
using WeatherLogger.Domain.Entities.Weather;

namespace WeatherLogger.Infrastructure.Persistence
{
    public class WeatherLoggerDbContext : DbContext
    {
        public WeatherLoggerDbContext(DbContextOptions<WeatherLoggerDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherRecord> WeatherRecords { get; set; } = null!;
        public DbSet<ApiLog> ApiLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiLog>()
                .Property(a => a.Endpoint)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
