using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherLogger.Application.Services;
using WeatherLogger.Domain.Entities.Log;
using WeatherLogger.Infrastructure.Persistence;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;
    private readonly WeatherLoggerDbContext _context;

    public WeatherController(WeatherService weatherService, WeatherLoggerDbContext context)
    {
        _weatherService = weatherService;
        _context = context;
    }

    [HttpGet("{city?}")]
    public async Task<IActionResult> GetWeather(string? city, CancellationToken cancellationToken)
    {
        var googleId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var record = await _weatherService.GetCurrentWeather(city, googleId, cancellationToken);

        if (record == null)
        {
            record = await _weatherService.GetWeatherAndSaveAsync(city, googleId, cancellationToken);
        }

        return Ok(record);
    }

    [HttpGet("history/{city}")]
    public async Task<IActionResult> GetCityHistory(string city, CancellationToken cancellationToken, int count = 10)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest("City name is required.");


        var history = await _weatherService.GetCityWeatherHistory(city, count, cancellationToken);
        if (history == null) return NotFound();

        return Ok(history);
    }


    [HttpPost("refresh/{city?}")]
    public async Task<IActionResult> RefreshWeather(string? city, CancellationToken cancellationToken)
    {
        var googleId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var record = await _weatherService.GetWeatherAndSaveAsync(city ?? "Sarajevo", googleId, cancellationToken);

        var log = new ApiLog
        {
            Endpoint = HttpContext.Request.Path,
            Method = HttpContext.Request.Method,
            StatusCode = 200,
            RequestBody = null,
            ResponseBody = $"City: {record.CityName}, Temp: {record.TemperatureCelsius}"
        };

        _context.ApiLogs.Add(log);
        await _context.SaveChangesAsync();

        return Ok(record);
    }

    [HttpGet("user/history")]
    public async Task<IActionResult> GetUserHistory(CancellationToken cancellationToken)
    {
        var googleId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var historyDto = await _weatherService.GetUserCityHistoryDtoAsync(googleId, cancellationToken);

        if (historyDto == null || !historyDto.Histories.Any())
            return NotFound();

        return Ok(historyDto);
    }
}
