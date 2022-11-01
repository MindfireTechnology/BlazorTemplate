using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Web.User.Services;
public class UserService : IUserService
{
	protected IUserClient UserClient { get; set; }
	protected ILogger<UserService> Logger { get; set; }

	public UserService(IUserClient client, ILogger<UserService> logger)
	{
		UserClient = client;
		Logger = logger;
	}

	public async Task<User?> GetUserByUsername(string username)
	{
		return await UserClient.GetUser(username);
	}

	public async Task UpdateAddress(Address address)
	{
		await UserClient.UpdateAddress(address);
	}
}
