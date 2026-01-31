using WeatherLogger.Application.DTOs;
using WeatherLogger.Domain.Entities.Weather;

namespace WeatherLogger.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherRecord> GetCurrentWeather(string city, string? googleId, CancellationToken cancellationToken);
        Task<WeatherRecord> GetWeatherAndSaveAsync(string city, string? googleId, CancellationToken cancellationToken);
        Task<IEnumerable<WeatherRecordHistory>> GetCityWeatherHistory(string city, int count, CancellationToken cancellationToken);
        Task<UserHistoryDto> GetUserCityHistoryDtoAsync(string googleId, CancellationToken cancellationToken);
    }

}
