using Microsoft.EntityFrameworkCore;
using WeatherLogger.Domain.Entities.Log;
using WeatherLogger.Domain.Entities.Weather;
using WeatherLogger.Infrastructure.Persistence.SeedData;


namespace WeatherLogger.Infrastructure.Persistence
{
    public class WeatherLoggerDbContext : DbContext
    {
        public WeatherLoggerDbContext(DbContextOptions<WeatherLoggerDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherRecord> WeatherRecords { get; set; } = null!;
        public DbSet<WeatherRecordHistory> WeatherRecordHistory { get; set; } = null!;
        public DbSet<ApiLog> ApiLogs { get; set; } = null!;
        public DbSet<GameCityWeather> GameCityWeathers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GameCityWeather>().HasData(GameCityWeatherSeeder.GetCities());

            modelBuilder.Entity<ApiLog>()
                    .Property(a => a.Endpoint)
                    .HasMaxLength(200)
                    .IsRequired();
        }
    }
}
