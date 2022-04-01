using Microsoft.Extensions.Logging;
using System;
using Data;
using EFRepository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Mindfire.User.Implementation;

public class UserDataService : IUserDataService
{
	protected ILogger<UserDataService> Logger { get; }
	protected IRepository Repository { get; }
	protected IMapper Mapper { get; }

	public UserDataService(ILogger<UserDataService> logger, IRepository repository, IMapper mapper)
	{
		Logger = logger;
		Repository = repository;
		Mapper = mapper;
	}

	public async Task<User?> GetUser(int? userId = null, string? email = null, string? userName = null)
	{
		try
		{
			var user = await Repository.Query<UserEntity>()
				.Include(x => x.Addresses)
				.ByUserId(userId)
				.ByEmail(email)
				.ByUserName(userName)
				.SingleOrDefaultAsync();

			return Mapper.Map<User>(user);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Unable to find user");
		}

		return default;
	}

	public async Task UpdateAddress(Address updated)
	{
		var existing = await Repository.FindOneAsync<AddressEntity>(updated.AddressId);

		if (existing == null)
			throw new InvalidOperationException("Address not found.");

		existing.UpdateAddressType(updated.AddressType!);
		existing.UpdateStreet1(updated.Street1!);
		existing.UpdateStreet2(updated.Street2!);
		existing.UpdateCity(updated.City!);
		existing.UpdateState(updated.State!);
		existing.UpdateZip(updated.Zip!);

		await Repository.SaveAsync();
	}
}
