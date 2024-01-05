using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R_WEB_PROJECT.Models.Login
{
	public class AccountModel
	{
		public AccountModel()
		{
			idx = 0;
			UserId = string.Empty;
			UserPassword = string.Empty;
			UserPasswordSalt = string.Empty;
			UserName = string.Empty;
		}

		[Key]
		[Required]
		public int idx { get; set; }

		[Required]
		[StringLength(30)]
		public string UserId { get; set; }

		[Required]
		[StringLength(50)]
		public string UserPassword { get; set; }

		[Required]
		[StringLength(50)]
		public string UserPasswordSalt { get; set; }

		[Required]
		[StringLength(20)]
		public string UserName { get; set; }

		public override string ToString()
		{
			return $"Idx[{idx}], UserId[{UserId}], UserName[{UserName}]";
		}

	}
}
