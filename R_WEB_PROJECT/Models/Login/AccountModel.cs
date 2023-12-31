using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R_WEB_PROJECT.Models.Login
{
	public class AccountModel
	{

		[Key]
		[Column("A_NO")]
		public int aNo { get; set; }

		[Column("A_ID")]
		public string aId { get; set; }

		[Column("A_PASSWORD")]
		public string aPassword { get; set; }

		[Column("A_NAME")]
		public string aName { get; set; }

		[Column("A_PASSWORD_SALT")]
		public string aPasswordSalt { get; set; }
	}
}
