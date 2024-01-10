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
			UserRoleCd = string.Empty;
			UserCreateAt = DateTime.Now;
		}

		[Key]
		[Required]
		public int idx { get; set; }

		[Required]
		[StringLength(50)]
		public string UserId { get; set; }

		[Required]
		[StringLength(255)]
		public string UserPassword { get; set; }

		[Required]
		[StringLength(255)]
		public string UserPasswordSalt { get; set; }

		[Required]
		[StringLength(30)]
		public string UserName { get; set; }

		[Required]
		[StringLength(20)]
		public string UserRoleCd { get; set; }

		[Required]
		public DateTime UserCreateAt { get; set; }

		public DateTime? UserUpdateAt { get; set; }

		public DateTime? UserDeleteAt { get; set; }

		public override string ToString()
		{
			return $"Idx[{idx}], UserId[{UserId}], UserName[{UserName}], UserRoleCd[{UserRoleCd}], UserCreateAt[{UserCreateAt}]," +
				$"UserUpdateAt[{UserUpdateAt}], UserDeleteAt[{UserDeleteAt}]";
		}

	}
}
