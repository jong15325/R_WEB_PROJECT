using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models.User;
using R_WEB_PROJECT.Repositories.Login;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.Utilities.password;
using static R_WEB_PROJECT.Utilities.Enums.LoginEnum;

namespace R_WEB_PROJECT.Services.Login
{
    public interface ILoginService
    {
        //아이디, 비밀번호로 계정 존재 여부 확인 서비스
        Task<AccountValidDTO> IsAccountByIdAsync(AccountModel model);
    }

    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;


        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        //아이디, 비밀번호로 계정 존재 여부 확인 서비스
        public async Task<AccountValidDTO> IsAccountByIdAsync(AccountModel model)
        {
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== IsAccountByIdPassAsync Start ===============================");

                bool isPass = false;
                string result = GetResultMessage(LoginResult.Success);

                var accountInfo = await _loginRepository.IsAccountByIdAsync(model);
                if (accountInfo != null)
                {
                    // 비밀번호 해시값 검증
                    var hashedPassword = PasswordManager.HashPassword(model.UserPassword, accountInfo.UserPasswordSalt, false);
                    LogUtil.Debug("SECURITY", $"hashedPassword : {hashedPassword}");

                    // 비밀번호 검증
                    if (PasswordManager.VerifyPassword(hashedPassword, accountInfo.UserPassword))
                        isPass = true;
                    else
                        result = GetResultMessage(LoginResult.PasswordMismatch);
                }
                else
                    result = GetResultMessage(LoginResult.NotFound);

                LogUtil.Debug("SYSTEM", "=============================== IsAccountByIdPassAsync End ===============================");

                return new AccountValidDTO { IsPass = isPass, AccountInfo = accountInfo, Result = result };
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during IsAccountByIdAsync Service : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
        }
    }
}
