using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.WeatherForecast.Services;
public class ForecastMapperConfiguration : Profile
{
	public ForecastMapperConfiguration()
	{
		CreateMap<WeatherForecast, WebApi.Models.WeatherForecast>()
			.ReverseMap();
	}
}
