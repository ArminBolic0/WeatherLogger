using WeatherLogger.Application.Services;

namespace WeatherLogger.Application.DTOs
{
    public class WeatherStackResponse
    {
        public Location? Location { get; set; }
        public Current? Current { get; set; }
    }

    public class Location
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

    public class Current
    {
        public double? Temperature { get; set; }
        public int? Humidity { get; set; }
        public double? Wind_Speed { get; set; }
        public string[]? Weather_Descriptions { get; set; }
    }
}
