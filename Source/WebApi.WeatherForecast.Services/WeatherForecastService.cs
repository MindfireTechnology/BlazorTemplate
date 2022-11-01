using Microsoft.Extensions.Logging;

namespace WebApi.WeatherForecast.Services;
public class WeatherForecastService : IWeatherForecastService
{
	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	protected ILogger<WeatherForecastService> Logger { get; set; }

	public WeatherForecastService(ILogger<WeatherForecastService> logger)
	{
		Logger = logger;
	}

	public IEnumerable<WeatherForecast> GetWeatherForecasts()
	{
		var rng = new Random();
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateTime.Now.AddDays(index),
			TemperatureC = rng.Next(-20, 55),
			Summary = Summaries[rng.Next(Summaries.Length)]
		})
		.ToArray();
	}
}
