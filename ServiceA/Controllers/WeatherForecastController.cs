using Microsoft.AspNetCore.Mvc;
using ServiceA.Publisher;

namespace ServiceA.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly PingPublisher _publisher;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, PingPublisher publisher)
    {
        _logger = logger;
        _publisher = publisher;
    }

    [HttpPost]
    public IActionResult Ping(string name)
    {
        _publisher.Publish(name);
        return NoContent();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
