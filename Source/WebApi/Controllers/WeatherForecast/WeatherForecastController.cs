using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.WeatherForecast;
using Api = WebApi.Models;

namespace WebApi.Controllers.WeatherForecast;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	protected ILogger<WeatherForecastController> Logger { get; set; }
	protected IWeatherForecastService ForecastService { get; set; }
	protected IMapper Mapper { get; set; }

	public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService forecastService, IMapper mapper)
	{
		Logger = logger;
		ForecastService = forecastService;
		Mapper = mapper;
	}

	[HttpGet("forecasts")]
	public ActionResult<IEnumerable<Api.WeatherForecast>> GetForecasts()
	{
		try
		{
			return Ok(Mapper.Map<IEnumerable<Api.WeatherForecast>>(ForecastService.GetWeatherForecasts()));
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error occurred fetching forecasts.");
			return StatusCode(StatusCodes.Status500InternalServerError);
		}
	}
}
