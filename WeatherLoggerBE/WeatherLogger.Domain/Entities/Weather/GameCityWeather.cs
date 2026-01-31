namespace WeatherLogger.Domain.Entities.Weather
{
    public class GameCityWeather : BaseEntity
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public double AverageTemperatureCelsius { get; set; }
    }
}
