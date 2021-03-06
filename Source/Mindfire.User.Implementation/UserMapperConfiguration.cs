using AutoMapper;
using Data;

namespace Mindfire.User.Implementation
{
	public class UserMapperConfiguration : Profile
	{
		public UserMapperConfiguration()
		{
			CreateMap<UserEntity, User>()
				.ReverseMap();

			CreateMap<AddressEntity, Address>()
				.ReverseMap();
		}
	}
}
