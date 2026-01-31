namespace WeatherLogger.Domain.Entities.User
{
    public class AppUser : BaseEntity
    {
        public int Id { get; set; }
        public string GoogleId { get; set; } = null!;
        public ICollection<UserCityHistory> CityHistories { get; set; } = new List<UserCityHistory>();
    }
}
