using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Web.WeatherForecast;
public interface IWeatherForecastClient
{
	[Get("/api/WeatherForecast/get")]
	public Task<IEnumerable<WeatherForecast>> GetForecasts();
}
