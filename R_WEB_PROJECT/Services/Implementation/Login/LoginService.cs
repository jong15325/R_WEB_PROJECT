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
		public async Task<bool> IsAccountByIdPassAsync(AccountModel model)
		{
			bool isPass = false;

			var accountInfo = await _loginRepository.IsAccountByIdPassAsync(model);
			if (accountInfo != null)
			{
				isPass = true;
				Log.Debug("SYSTEM", $"Retrieved aId = {accountInfo.aId}");
				Log.Debug("SYSTEM", $"Retrieved aPassword = {accountInfo.aPassword}");
				Log.Debug("SYSTEM", $"Retrieved aName = {accountInfo.aName}");
				Log.Debug("SYSTEM", $"Retrieved aPasswordSalt = {accountInfo.aPasswordSalt}");

				// 비밀번호 해시값 검증
				var hashedPassword = PasswordHasher.HashPassword(model.aPassword, accountInfo.aPasswordSalt, false);
				Console.WriteLine(hashedPassword);
				if (hashedPassword == accountInfo.aPassword)
				{
					isPass = true;
				}
			}

			return isPass;
		}
	}
}
