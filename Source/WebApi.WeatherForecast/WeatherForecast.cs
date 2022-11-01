namespace WebApi.WeatherForecast;
public record WeatherForecast
{
	public DateTime Date { get; set; }

	public int TemperatureC { get; set; }

	public string? Summary { get; set; }
}
