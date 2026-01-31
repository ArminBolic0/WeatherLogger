namespace WeatherLogger.Application.DTOs
{
    public class CityHistoryDto
    {
        public string CityName { get; set; }
        public DateTime ObservationTime { get; set; }
        public double TemperatureCelsius { get; set; }
    }

    public class UserHistoryDto
    {
        public int id { get; set; }
        public List<CityHistoryDto> Histories { get; set; }
    }
}
