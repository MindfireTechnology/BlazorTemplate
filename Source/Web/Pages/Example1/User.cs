using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Pages.Example1
{
	public class User
	{
		public int? UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public DateTime? Birthdate { get; set; }

		public List<Address> Addresses { get; set; } = new List<Address>();
	}

	public class Address
	{
		public string AddressType { get; set; }
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
	}
}
