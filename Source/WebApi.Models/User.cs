using System;
using System.Collections.Generic;

namespace WebApi.Models;

public record User
{
	public int UserId { get; set; }
	public string UserName { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }

	public DateTime? Birthdate { get; set; }

	public List<Address> Addresses { get; set; } = new List<Address>();
}
