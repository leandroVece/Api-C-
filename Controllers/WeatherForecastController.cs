using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    List<WeatherForecast> listWF = new List<WeatherForecast>();

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        if (listWF == null || !listWF.Any())
        {
            listWF = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Route("Get/weatherforecast")]
    [Route("[action]")]
    public IEnumerable<WeatherForecast> NewGet()
    {
        _logger.LogInformation("Soy un log");
        return listWF;
    }

    [HttpPost]
    public IActionResult Post(WeatherForecast watherf)
    {
        listWF.Add(watherf);
        return Ok();
    }


    [HttpDelete("{index}")]
    public IActionResult Delete(int index)
    {
        listWF.RemoveAt(index);
        return Ok();
    }


}
