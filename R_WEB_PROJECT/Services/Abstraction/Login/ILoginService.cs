using R_WEB_PROJECT.DTOs.Login;
using R_WEB_PROJECT.Models.Login;
using System.Data;

namespace R_WEB_PROJECT.Services.Abstraction.Login
{
	public interface ILoginService
	{
		//아이디, 비밀번호로 계정 존재 여부 확인 서비스
		Task<AccountValidDTO> IsAccountByIdPassAsync(AccountModel model);
	}
}
