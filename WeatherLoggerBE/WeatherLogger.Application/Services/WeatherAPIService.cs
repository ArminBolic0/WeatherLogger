using Microsoft.EntityFrameworkCore;
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

        public async Task<WeatherRecord> GetCurrentWeather(string city, CancellationToken cancellationToken)
        {
            return await _context.WeatherRecords.Where(wr => wr.CityName == city).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<WeatherRecordHistory>> GetCityWeatherHistory(string city, int count, CancellationToken cancellationToken)
        {
            var result = await _context.WeatherRecordHistory
            .Where(w => w.CityName.ToLower() == city.ToLower())
            .OrderByDescending(w => w.ObservationTime)
            .Take(count)
            .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<WeatherRecord> GetWeatherAndSaveAsync(string city, CancellationToken cancellationToken)
        {
            var url = $"https://api.weatherstack.com/current?access_key={_apiKey}&query={city}";
            var response = await _httpClient.GetFromJsonAsync<WeatherStackResponseDto>(url);

            if (response == null) throw new Exception("Failed to get weather");

            var newRecord = new WeatherRecord
            {
                CityName = response.Location.Name ?? "Unknown",
                CountryName = response.Location.Country ?? "Unknown",
                TemperatureCelsius = response.Current.Temperature ?? 0,
                Humidity = response.Current.Humidity ?? 0,
                WindSpeed = response.Current.Wind_Speed ?? 0,
                WeatherDescription = response.Current.Weather_Descriptions?.FirstOrDefault() ?? "N/A",
                ObservationTime = DateTime.Now
            };

            var existingRecord = await GetCurrentWeather(city, cancellationToken);

            if (existingRecord != null)
            {
                var historyRecord = new WeatherRecordHistory
                {
                    CityName = existingRecord.CityName,
                    CountryName = existingRecord.CountryName,
                    TemperatureCelsius = existingRecord.TemperatureCelsius,
                    ObservationTime = existingRecord.ObservationTime
                };
                _context.WeatherRecordHistory.Add(historyRecord);

                existingRecord.TemperatureCelsius = newRecord.TemperatureCelsius;
                existingRecord.Humidity = newRecord.Humidity;
                existingRecord.WindSpeed = newRecord.WindSpeed;
                existingRecord.WeatherDescription = newRecord.WeatherDescription;
                existingRecord.ObservationTime = newRecord.ObservationTime;
            }
            else
            {
                _context.WeatherRecords.Add(newRecord);
            }


            await _context.SaveChangesAsync(cancellationToken);

            return newRecord;
        }
    }

}
