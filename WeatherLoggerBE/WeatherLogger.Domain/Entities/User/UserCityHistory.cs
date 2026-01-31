namespace WeatherLogger.Domain.Entities.User
{
    public class UserCityHistory : BaseEntity
    {
        public int Id { get; set; }
        public string CityName { get; set; } = null!;
        public DateTime CheckedAt { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
