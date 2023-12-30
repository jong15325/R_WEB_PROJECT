using System.ComponentModel.DataAnnotations;

namespace R_WEB_PROJECT.Models.Login
{
	public class AccountModel
	{

		[Key]
		public int aNo { get; set; }

		public string aId { get; set; }

		public string aPassword { get; set; }

		public string aName { get; set; }

		public string APasswordSalt { get; set; }
	}
}
