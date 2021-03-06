using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
	[Table("User")]
	public class UserEntity
	{
		[Key]
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public DateTime? Birthdate { get; set; }
		public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
		public DateTimeOffset? Modified { get; set; }

		public ICollection<AddressEntity> Addresses { get; set; }
	}
}
