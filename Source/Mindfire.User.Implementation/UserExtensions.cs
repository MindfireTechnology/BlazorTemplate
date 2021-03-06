using Data;
using System.Linq;

namespace Mindfire.User.Implementation
{
	public static class UserExtensions
	{
		public static IQueryable<UserEntity> ByUsername(this IQueryable<UserEntity> userRepository, string name)
		{
			if (name == null)
				return userRepository;

			return userRepository.Where(x => x.UserName == name);
		}

		public static IQueryable<UserEntity> ByEmail(this IQueryable<UserEntity> userRepository, string email)
		{
			if (email == null)
				return userRepository;

			return userRepository.Where(x => x.Email == email);
		}

		public static IQueryable<UserEntity> ByUserId(this IQueryable<UserEntity> userRepository, int? userId)
		{
			if (userId == null)
				return userRepository;

			return userRepository.Where(x => x.UserId == userId);
		}

		public static AddressEntity UpdateAddressType(this AddressEntity address, string addressType)
		{
			if (string.IsNullOrWhiteSpace(addressType) || address.AddressType == addressType)
				return address;

			address.AddressType = addressType;

			return address;
		}

		public static AddressEntity UpdateStreet1(this AddressEntity address, string street1)
		{
			if (string.IsNullOrWhiteSpace(street1) || address.Street1 == street1)
				return address;

			address.Street1 = street1;

			return address;
		}

		public static AddressEntity UpdateStreet2(this AddressEntity address, string street2)
		{
			if (string.IsNullOrWhiteSpace(street2) || address.Street2 == street2)
				return address;

			address.Street2 = street2;

			return address;
		}

		public static AddressEntity UpdateCity(this AddressEntity address, string city)
		{
			if (string.IsNullOrWhiteSpace(city) || address.City == city)
				return address;

			address.City = city;

			return address;
		}

		public static AddressEntity UpdateState(this AddressEntity address, string state)
		{
			if (string.IsNullOrWhiteSpace(state) || address.State == state)
				return address;

			address.State = state;

			return address;
		}

		public static AddressEntity UpdateZip(this AddressEntity address, string zip)
		{
			if (string.IsNullOrWhiteSpace(zip) || address.Zip == zip)
				return address;

			address.Zip = zip;

			return address;
		}
	}
}
