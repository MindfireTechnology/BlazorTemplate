using AutoMapper;

namespace Web.User.Services;

public class UserMapperConfiguration : Profile
{
	public UserMapperConfiguration()
	{
		CreateMap<User, WebApi.Models.User>()
			.ReverseMap();

		CreateMap<Address, WebApi.Models.Address>()
			.ReverseMap();
	}
}
