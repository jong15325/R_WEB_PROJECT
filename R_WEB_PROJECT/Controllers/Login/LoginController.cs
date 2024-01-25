using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Controllers.Main;
using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models.Log;
using R_WEB_PROJECT.Models.User;
using R_WEB_PROJECT.Services.Log;
using R_WEB_PROJECT.Services.Login;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.Utilities.Manager;
using R_WEB_PROJECT.Utilities.Redis;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Controllers.Login
{
    public class LoginController : Controller
    { 
		private readonly ILoginService _loginService;
        private readonly ILogLoginService _logLoginService;
        private readonly RedisManager _redisSessionStore;
        private readonly MessageManager _messageManager;

        public LoginController(ILoginService loginService, ILogLoginService logLoginService, RedisManager redisSessionStore, MessageManager messageManager)
		{
			_loginService = loginService;
            _logLoginService = logLoginService;
            _redisSessionStore = redisSessionStore;
            _messageManager = messageManager;
        }

        //로그인 메인 페이지
        [Route("/login/main")]
        public IActionResult Login(AccountModel model)
        {
			try
			{
				LogUtil.Debug("SYSTEM", "=============================== LoginPage Start ===============================");
				LogUtil.Debug("SYSTEM", "=============================== LoginPage End ===============================");
			}
			catch (Exception ex)
			{
				LogUtil.Error("SYSTEM", $"An error occurred during login: {ex.Message}", ex);
			}
			
			return View("login_main", model);
        }

		//로그인 프로세스
        [Route("/login/loginAction")]
        [HttpPost] // POST 메서드를 통해 폼 데이터를 처리
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LoginAction(AccountModel model)
        {
			try
			{
				LogUtil.Debug("SYSTEM", "=============================== LoginAction Start ===============================");

				if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.UserPassword))
				{
					// UserId 또는 UserPassword가 비어있는 경우 처리
					LogUtil.Warn("SYSTEM", "아이디 또는 비밀번호를 입력하지 않고 로그인을 시도했습니다.");
					LogUtil.Debug("SYSTEM", "=============================== LoginAction End ===============================");

                    AlertManager.BasicAlert(this, "", _messageManager.GetMessage("Login_EnterIdPasswd"), AlertIconType.warning);
                    return RedirectToAction(nameof(Login), "Login");
                }

				//로그인 검증
				AccountValidDTO isAccountPass = await _loginService.IsAccountByIdAsync(model);
				LogUtil.Info("SYSTEM", $"로그인 시도 UserId = {model.UserId} / 아이디 검증 결과 = {isAccountPass.IsPass}");

				if (isAccountPass.IsPass)
				{
					try
					{
                        //레디스 세션 저장
                        await _redisSessionStore.SetRedisAsync($"userSession:{isAccountPass.AccountInfo.Idx}", new AccountModel
                        {
                            Idx = isAccountPass.AccountInfo.Idx,
                            UserId = isAccountPass.AccountInfo.UserId,
                            UserType = isAccountPass.AccountInfo.UserType,
                            UserName = isAccountPass.AccountInfo.UserName,
                            UserRoleCd = isAccountPass.AccountInfo.UserRoleCd

                        }, TimeSpan.FromMinutes(30));
                    }
					catch (Exception ex)
					{
                        LogUtil.Error("REDIS", $"An error occurred while saving the Redis session : {ex.GetType().Name} - {ex.Message}", ex);
                        AlertManager.BasicAlert(this, "", _messageManager.GetMessage("Login_Error"), AlertIconType.error);
                        return RedirectToAction(nameof(Login), "Login");
                    }

                    //로그인 이력 저장
                    await _logLoginService.InsertLogLoginAsync(new LogLoginModel { });

                    LogUtil.Info("SYSTEM", $"{isAccountPass.Result} - {isAccountPass.AccountInfo.ToString()}");
                    LogUtil.Debug("SYSTEM", "=============================== LoginAction End ===============================");

                    AlertManager.MixinAlert(this, _messageManager.GetMessage("Login_Success"), "", AlertIconType.success);
                    return RedirectToAction(nameof(MainController.Main), "Main");
                }

                //아이디가 존재하지 않거나 비밀번호가 존재하지 않을 경우
                LogUtil.Info("SYSTEM", $"{isAccountPass.Result} - {model.ToString()}");
            }
            catch (Exception ex) 
			{
				LogUtil.Error("SYSTEM", $"An error occurred during login : {ex.GetType().Name} - {ex.Message}", ex);

                AlertManager.BasicAlert(this, "", _messageManager.GetMessage("Login_Error"), AlertIconType.error);
                return RedirectToAction(nameof(Login), "Login", new AccountModel { UserId = model.UserId });
            }

            LogUtil.Debug("SYSTEM", "=============================== LoginAction End ===============================");

            AlertManager.BasicAlert(this, "", _messageManager.GetMessage("Login_Invaild"), AlertIconType.warning);
            return RedirectToAction(nameof(Login), "Login", new AccountModel { UserId = model.UserId });
        }
    }
}
