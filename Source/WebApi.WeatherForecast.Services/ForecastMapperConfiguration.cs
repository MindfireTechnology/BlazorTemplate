using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.WeatherForecast.Services;
public class ForecastMapperConfiguration : Profile
{
	public ForecastMapperConfiguration()
	{
		CreateMap<WeatherForecast, Models.WeatherForecast>()
			.ReverseMap();
	}
}
