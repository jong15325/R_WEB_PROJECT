using R_WEB_PROJECT.DTOs.Login;
using R_WEB_PROJECT.Models.Login;
using R_WEB_PROJECT.Repositories.Abstraction.Login;
using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.Utilities.password;

namespace R_WEB_PROJECT.Services.Implementation.Login
{
    public class LoginService : ILoginService
	{
		private readonly ILoginRepository _loginRepository;


		public LoginService(ILoginRepository loginRepository)
		{
			_loginRepository = loginRepository;
		}

		//아이디, 비밀번호로 계정 존재 여부 확인 서비스
		public async Task<AccountValidDTO> IsAccountByIdPassAsync(AccountModel model)
		{
			Log.Debug("SYSTEM", "=============================== IsAccountByIdPassAsync Start ===============================");

			bool isPass = false;

			var accountInfo = await _loginRepository.IsAccountByIdPassAsync(model);
			if (accountInfo != null)
			{
				Log.Debug("SYSTEM", $"Retrieved idx = {accountInfo.idx}");
				Log.Debug("SYSTEM", $"Retrieved UserId = {accountInfo.UserId}");
				Log.Debug("SYSTEM", $"Retrieved UserName = {accountInfo.UserName}");

				// 비밀번호 해시값 검증
				var hashedPassword = PasswordManager.HashPassword(model.UserPassword, accountInfo.UserPasswordSalt, true);
				Log.Debug("SECURITY", $"hashedPassword : {hashedPassword}");

				// 비밀번호 검증
				if (PasswordManager.VerifyPassword(hashedPassword, accountInfo.UserPassword))
					isPass = true;
			}

			Log.Debug("SYSTEM", $"isPass : {isPass}");
			Log.Debug("SYSTEM", "=============================== IsAccountByIdPassAsync End ===============================");

			return new AccountValidDTO { IsPass = isPass, AccountInfo = accountInfo };
		}
	}
}
