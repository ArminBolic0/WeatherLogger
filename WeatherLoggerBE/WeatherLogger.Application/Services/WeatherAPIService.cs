using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using WeatherLogger.Application.DTOs;
using WeatherLogger.Domain.Entities.User;
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

        public async Task<WeatherRecord> GetCurrentWeather(string city, string? googleId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(googleId))
            {
                var user = await _context.AppUsers
                    .FirstOrDefaultAsync(u => u.GoogleId == googleId, cancellationToken);

                if (user == null)
                {
                    user = new AppUser
                    {
                        GoogleId = googleId,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.AppUsers.Add(user);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                _context.UserCityHistories.Add(new UserCityHistory
                {
                    AppUserId = user.Id,
                    CityName = city,
                    CheckedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await _context.WeatherRecords.Where(wr => wr.CityName == city).FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<WeatherRecord> GetCurrentWeatherHelper(string city, CancellationToken cancellationToken)
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


            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<WeatherRecord> GetWeatherAndSaveAsync(string city, string? googleId, CancellationToken cancellationToken)
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

            var existingRecord = await GetCurrentWeatherHelper(city, cancellationToken);

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

            return existingRecord ?? newRecord;
        }


        public async Task<UserHistoryDto> GetUserCityHistoryDtoAsync(string googleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(googleId))
                return null;

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.GoogleId == googleId, cancellationToken);

            if (user == null)
                return null;

            var histories = await _context.UserCityHistories
                .Where(h => h.AppUserId == user.Id)
                .OrderByDescending(h => h.CheckedAt)
                .Select(h => new CityHistoryDto
                {
                    CityName = h.CityName,
                    ObservationTime = h.CheckedAt,
                })
                .ToListAsync(cancellationToken);

            return new UserHistoryDto
            {
                id = user.Id,
                Histories = histories
            };
        }

    }

}
