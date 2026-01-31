using Microsoft.AspNetCore.Mvc;
using WeatherLogger.Application.DTOs;
using WeatherLogger.Application.Interfaces;
using WeatherLogger.Domain.Entities.Log;
using WeatherLogger.Infrastructure.Persistence;

namespace WeatherLogger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly WeatherLoggerDbContext _context;

        public GameController(IGameService gameService, WeatherLoggerDbContext context)
        {
            _gameService = gameService;
            _context = context;
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
                var log = new ApiLog
                {
                    Endpoint = HttpContext.Request.Path,
                    Method = HttpContext.Request.Method,
                    StatusCode = 200,
                    RequestBody = null,
                    ResponseBody = $"Current city: {result.CurrentCity}, Next city: {result.NextCity}"
                };

                _context.ApiLogs.Add(log);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
