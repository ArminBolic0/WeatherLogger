using System.Net.Http.Json;
using WeatherLogger.Application.DTOs;
using WeatherLogger.Domain.Entities.Weather;
using WeatherLogger.Infrastructure.Persistence;

namespace WeatherLogger.Application.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherLoggerDbContext _context;
        private readonly string _apiKey = "69457ea1cf6f6e6279631139671e2715";

        public WeatherService(HttpClient httpClient, WeatherLoggerDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<WeatherRecord> GetWeatherAndSaveAsync(string city = "Sarajevo")
        {
            var url = $"https://api.weatherstack.com/current?access_key={_apiKey}&query={city}";
            var response = await _httpClient.GetFromJsonAsync<WeatherStackResponse>(url);

            if (response == null) throw new Exception("Failed to get weather");

            var record = new WeatherRecord
            {
                CityName = response.Location.Name ?? "Unknown",
                CountryName = response.Location.Country ?? "Unknown",
                TemperatureCelsius = response.Current.Temperature ?? 0,
                Humidity = response.Current.Humidity ?? 0,
                WindSpeed = response.Current.Wind_Speed ?? 0,
                WeatherDescription = response.Current.Weather_Descriptions?.FirstOrDefault() ?? "N/A",
                ObservationTime = DateTime.UtcNow
            };

            _context.WeatherRecords.Add(record);
            await _context.SaveChangesAsync();

            return record;
        }
    }

}
