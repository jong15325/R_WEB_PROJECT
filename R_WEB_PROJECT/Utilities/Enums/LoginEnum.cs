using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
	public class LoginEnum
	{
		public enum LoginResult
		{
			[Description("Success")]
			Success,

			[Description("NotFound")]
			NotFound,

			[Description("PasswordMismatch")]
			PasswordMismatch,

			[Description("AccountLocked")]
			AccountLocked
		}

		public static string GetResultMessage(LoginResult type)
		{
			switch (type)
			{
				case LoginResult.Success:
					return "로그인에 성공했습니다";
				case LoginResult.NotFound:
					return "아이디를 찾을 수 없습니다";
				case LoginResult.PasswordMismatch:
					return "비밀번호가 일치하지 않습니다";
				case LoginResult.AccountLocked:
					return "잠긴 계정입니다";
				default:
					return "알 수 없는 오류입니다";
			}
		}
	}
}
