using Refit;

namespace Web.User;
public interface IUserClient
{
	[Get("/api/user/{username}")]
	public Task<User?> GetUser(string username);

	[Put("/api/user/address")]
	public Task UpdateAddress([Body] Address address);
}
