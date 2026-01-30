namespace WeatherLogger.Domain.Entities.Weather
{
    public class WeatherRecord: BaseEntity
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public double TemperatureCelsius { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string WeatherDescription { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}
