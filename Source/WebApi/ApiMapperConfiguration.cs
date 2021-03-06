using AutoMapper;
using Mindfire.User;
using Api = WebApi.Models;

namespace WebApi
{
	public class ApiMapperConfiguration : Profile
	{
		public ApiMapperConfiguration()
		{
			CreateMap<User, Api.User>()
				.ReverseMap();

			CreateMap<Address, Api.Address>()
				.ReverseMap();
		}
	}
}
