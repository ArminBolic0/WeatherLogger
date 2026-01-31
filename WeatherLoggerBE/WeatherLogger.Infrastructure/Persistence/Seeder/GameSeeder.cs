using WeatherLogger.Domain.Entities.Weather;

namespace WeatherLogger.Infrastructure.Persistence.SeedData
{
    public static class GameCityWeatherSeeder
    {
        public static List<GameCityWeather> GetCities()
        {
            return new List<GameCityWeather>
            {
                new GameCityWeather { Id = 1, CityName = "Sarajevo", CountryName = "BA", AverageTemperatureCelsius = 12 },
                new GameCityWeather { Id = 2,CityName = "London", CountryName = "UK", AverageTemperatureCelsius = 10 },
                new GameCityWeather { Id = 3,CityName = "Cairo", CountryName = "EG", AverageTemperatureCelsius = 28 },
                new GameCityWeather { Id = 4,CityName = "Oslo", CountryName = "NO", AverageTemperatureCelsius = 4 },
                new GameCityWeather { Id = 5,CityName = "Madrid", CountryName = "ES", AverageTemperatureCelsius = 18 },
                new GameCityWeather { Id = 6,CityName = "Paris", CountryName = "FR", AverageTemperatureCelsius = 12 },
                new GameCityWeather { Id = 7,CityName = "Berlin", CountryName = "DE", AverageTemperatureCelsius = 10 },
                new GameCityWeather { Id = 8,CityName = "Rome", CountryName = "IT", AverageTemperatureCelsius = 16 },
                new GameCityWeather { Id = 9,CityName = "Athens", CountryName = "GR", AverageTemperatureCelsius = 20 },
                new GameCityWeather { Id = 10,CityName = "Moscow", CountryName = "RU", AverageTemperatureCelsius = 5 },
                new GameCityWeather { Id = 11,CityName = "Tokyo", CountryName = "JP", AverageTemperatureCelsius = 15 },
                new GameCityWeather { Id = 12,CityName = "Sydney", CountryName = "AU", AverageTemperatureCelsius = 22 },
                new GameCityWeather { Id = 13,CityName = "Beijing", CountryName = "CN", AverageTemperatureCelsius = 13 },
                new GameCityWeather { Id = 14,CityName = "New York", CountryName = "US", AverageTemperatureCelsius = 12 },
                new GameCityWeather { Id = 15,CityName = "Los Angeles", CountryName = "US", AverageTemperatureCelsius = 19 },
                new GameCityWeather { Id = 16,CityName = "Toronto", CountryName = "CA", AverageTemperatureCelsius = 7 },
                new GameCityWeather { Id = 17,CityName = "Vancouver", CountryName = "CA", AverageTemperatureCelsius = 10 },
                new GameCityWeather { Id = 18,CityName = "Mexico City", CountryName = "MX", AverageTemperatureCelsius = 17 },
                new GameCityWeather { Id = 19,CityName = "Rio de Janeiro", CountryName = "BR", AverageTemperatureCelsius = 26 },
                new GameCityWeather { Id = 20,CityName = "Buenos Aires", CountryName = "AR", AverageTemperatureCelsius = 18 },
                new GameCityWeather { Id = 21,CityName = "Cape Town", CountryName = "ZA", AverageTemperatureCelsius = 17 },
                new GameCityWeather { Id = 22,CityName = "Johannesburg", CountryName = "ZA", AverageTemperatureCelsius = 16 },
                new GameCityWeather { Id = 23,CityName = "Istanbul", CountryName = "TR", AverageTemperatureCelsius = 15 },
                new GameCityWeather { Id = 24, CityName = "Dubai", CountryName = "AE", AverageTemperatureCelsius = 30 },
                new GameCityWeather { Id = 25,CityName = "Singapore", CountryName = "SG", AverageTemperatureCelsius = 28 },
                new GameCityWeather { Id = 26,CityName = "Bangkok", CountryName = "TH", AverageTemperatureCelsius = 29 },
                new GameCityWeather { Id = 27, CityName = "Delhi", CountryName = "IN", AverageTemperatureCelsius = 25 },
                new GameCityWeather { Id = 28,CityName = "Seoul", CountryName = "KR", AverageTemperatureCelsius = 13 },
                new GameCityWeather { Id = 29,CityName = "Helsinki", CountryName = "FI", AverageTemperatureCelsius = 3 },
                new GameCityWeather { Id = 30,CityName = "Stockholm", CountryName = "SE", AverageTemperatureCelsius = 5 },
                new GameCityWeather { Id = 31,CityName = "Warsaw", CountryName = "PL", AverageTemperatureCelsius = 8 },
                new GameCityWeather { Id = 32,CityName = "Lisbon", CountryName = "PT", AverageTemperatureCelsius = 16 },
                new GameCityWeather { Id = 33,CityName = "Budapest", CountryName = "HU", AverageTemperatureCelsius = 11 },
                new GameCityWeather { Id = 34,CityName = "Prague", CountryName = "CZ", AverageTemperatureCelsius = 9 },
                new GameCityWeather { Id = 35,CityName = "Kuala Lumpur", CountryName = "MY", AverageTemperatureCelsius = 28 },
                new GameCityWeather { Id = 36,CityName = "Lagos", CountryName = "NG", AverageTemperatureCelsius = 27 }
            };
        }
    }
}
