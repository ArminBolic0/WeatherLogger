using WeatherLogger.Domain.Entities.Weather;
using WeatherLogger.Application.DTOs;

namespace WeatherLogger.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameCityWeather> GetRandomCityAsync(CancellationToken cancellationToken);
        Task<GameGuessResultDto> CheckGuessAsync(GameGuessDto guessDto, CancellationToken cancellationToken);
    }
}
