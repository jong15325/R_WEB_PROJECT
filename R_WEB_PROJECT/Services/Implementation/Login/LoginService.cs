using R_WEB_PROJECT.Models.Login;
using R_WEB_PROJECT.Repositories.Abstraction.Login;
using R_WEB_PROJECT.Services.Abstraction.Login;

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
			return await _loginRepository.IsAccountByIdPassAsync(model);
		}
	}
}
