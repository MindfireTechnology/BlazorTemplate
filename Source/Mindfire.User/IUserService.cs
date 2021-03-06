using System.Threading.Tasks;

namespace Mindfire.User
{
	public interface IUserDataService
	{
		Task<User> GetUser(int? userId = null, string email = null, string userName = null);
		Task UpdateAddress(Address updated);
	}
}
