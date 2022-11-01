using System;

namespace WebApi.Models;
public record WeatherForecast
{
	public DateTime Date { get; set; }

	public int TemperatureC { get; set; }

	public string? Summary { get; set; }
}
