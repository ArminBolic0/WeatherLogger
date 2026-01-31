namespace WeatherLogger.Domain.Entities.Weather
{
    public class WeatherRecordHistory: BaseEntity
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public double TemperatureCelsius { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}
