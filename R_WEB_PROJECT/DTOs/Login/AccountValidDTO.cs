using R_WEB_PROJECT.Models.Login;

namespace R_WEB_PROJECT.DTOs.Login
{
	public class AccountValidDTO
	{
		public bool IsPass { get; set; }
		public AccountModel AccountInfo { get; set; }
	}
}
