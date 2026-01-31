namespace WeatherLogger.Application.DTOs
{
        public class GameGuessDto
        {
            public string CurrentCity { get; set; } = null!;
            public string NextCity { get; set; } = null!;
            public bool IsHigherGuess { get; set; } 
        }

        public class GameGuessResultDto
        {
            public bool Correct { get; set; }
            public string CurrentCity { get; set; } = null!;
            public double CurrentTemperature { get; set; }
            public string NextCity { get; set; } = null!;
            public double NextTemperature { get; set; }
        }
}
