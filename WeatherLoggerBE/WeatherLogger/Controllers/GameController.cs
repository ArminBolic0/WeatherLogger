using Microsoft.AspNetCore.Mvc;
using WeatherLogger.Application.Services;
using WeatherLogger.Application.DTOs;

namespace WeatherLogger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomCity(CancellationToken cancellationToken)
        {
            try
            {
                var city = await _gameService.GetRandomCityAsync(cancellationToken);
                return Ok(city);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("check")]
        public async Task<IActionResult> CheckGuess([FromBody] GameGuessDto guessDto, CancellationToken cancellationToken)
        {
            if (guessDto == null)
                return BadRequest(new { error = "Invalid input" });

            try
            {
                var result = await _gameService.CheckGuessAsync(guessDto, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
