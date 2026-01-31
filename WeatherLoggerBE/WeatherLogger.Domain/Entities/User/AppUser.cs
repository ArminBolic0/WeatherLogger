namespace WeatherLogger.Domain.Entities.User
{
    public class AppUser : BaseEntity
    {
        public string GoogleId { get; set; } = null!;
        public ICollection<UserCityHistory> CityHistories { get; set; } = new List<UserCityHistory>();
    }
}
