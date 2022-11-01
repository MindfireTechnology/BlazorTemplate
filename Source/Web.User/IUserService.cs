using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.User;
public interface IUserService
{
	public Task<User?> GetUserByUsername(string username);
	public Task UpdateAddress(Address address);
}
