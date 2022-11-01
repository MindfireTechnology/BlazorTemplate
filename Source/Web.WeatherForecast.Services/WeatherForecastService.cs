using Microsoft.Extensions.Logging;
using Web.WeatherForecast;

namespace Web.WeatherForecast.Services;
public class WeatherForecastService : IWeatherForecastService
{
	protected ILogger<WeatherForecastService> Logger { get; set; }
	protected IWeatherForecastClient WeatherForecastClient { get; set; }

	public WeatherForecastService(ILogger<WeatherForecastService> logger, IWeatherForecastClient weatherForecastClient)
	{
		Logger = logger;
		WeatherForecastClient = weatherForecastClient;
	}

	public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
	{
		return await WeatherForecastClient.GetForecasts();
	}
}
