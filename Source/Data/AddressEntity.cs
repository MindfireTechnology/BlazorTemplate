using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
	[Table("Address")]
	public class AddressEntity
	{
		[Key]
		public int AddressId { get; set; }
		public int UserId { get; set; }
		public string AddressType { get; set; }
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		public UserEntity User { get; set; }
	}
}
