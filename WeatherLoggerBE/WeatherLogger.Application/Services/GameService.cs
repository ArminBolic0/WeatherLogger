using Microsoft.EntityFrameworkCore;
using WeatherLogger.Domain.Entities.Weather;
using WeatherLogger.Infrastructure.Persistence;
using WeatherLogger.Application.DTOs;
using WeatherLogger.Application.Interfaces;

namespace WeatherLogger.Application.Services
{
    public class GameService: IGameService
    {
        private readonly WeatherLoggerDbContext _context;
        private readonly Random _random;

        public GameService(WeatherLoggerDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<GameCityWeather> GetRandomCityAsync(CancellationToken cancellationToken)
        {
            var cities = await _context.GameCityWeathers.ToListAsync(cancellationToken);
            if (!cities.Any()) throw new Exception("No game cities found.");

            return cities[_random.Next(cities.Count)];
        }
        public async Task<GameGuessResultDto> CheckGuessAsync(GameGuessDto guessDto, CancellationToken cancellationToken)
        {
            var current = await _context.GameCityWeathers
                .FirstOrDefaultAsync(c => c.CityName.ToLower() == guessDto.CurrentCity.ToLower(), cancellationToken);

            var next = await _context.GameCityWeathers
                .FirstOrDefaultAsync(c => c.CityName.ToLower() == guessDto.NextCity.ToLower(), cancellationToken);

            if (current == null || next == null)
                throw new Exception("One of the cities not found in game data.");

            bool actualHigher = next.AverageTemperatureCelsius > current.AverageTemperatureCelsius;
            bool correct = actualHigher == guessDto.IsHigherGuess;

            return new GameGuessResultDto
            {
                Correct = correct,
                CurrentCity = current.CityName,
                CurrentTemperature = current.AverageTemperatureCelsius,
                NextCity = next.CityName,
                NextTemperature = next.AverageTemperatureCelsius
            };
        }
    }
}
