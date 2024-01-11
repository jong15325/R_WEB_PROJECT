using log4net;
using log4net.Util;
using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Controllers.Main;
using R_WEB_PROJECT.DTOs.Login;
using R_WEB_PROJECT.Models.Login;
using R_WEB_PROJECT.RedisStore.Session;
using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Log;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace R_WEB_PROJECT.Controllers.Login
{
    public class LoginController : Controller
    { 
		private readonly ILoginService _loginService;
		private readonly RedisSessionStore _redisSessionStore;

		public LoginController(ILoginService loginService, RedisSessionStore redisSessionStore)
		{
			_loginService = loginService;
			_redisSessionStore = redisSessionStore;
		}

		//로그인 메인 페이지
		[Route("/login/main")]
        public IActionResult Login()
        {
			try
			{
				Log.Debug("SYSTEM", "=============================== LoginPage Start ===============================");
				Log.Debug("SYSTEM", "=============================== LoginPage End ===============================");
			}
			catch (Exception ex)
			{
				Log.Error("SYSTEM", $"An error occurred during login: {ex.Message}", ex);
			}
			
			return View("login_main");
        }

		//로그인 프로세스
        [Route("/login/loginAction")]
        [HttpPost] // POST 메서드를 통해 폼 데이터를 처리
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LoginAction(AccountModel model)
        {
			try
			{
				Log.Debug("SYSTEM", "=============================== LoginAction Start ===============================");

				//로그인 검증
				AccountValidDTO isAccountPass = await _loginService.IsAccountByIdAsync(model);
				Log.Info("SYSTEM", $"로그인 시도 UserId = {model.UserId} / 아이디 검증 결과 = {isAccountPass.IsPass}");

				if (isAccountPass.IsPass)
				{
					//레디스 세션 저장
					await _redisSessionStore.SetSessionAsync($"userSession:{isAccountPass.AccountInfo.Idx}", new AccountModel { 
						Idx = isAccountPass.AccountInfo.Idx,
						UserId = isAccountPass.AccountInfo.UserId,
						UserType = isAccountPass.AccountInfo.UserType,
						UserName = isAccountPass.AccountInfo.UserName,
						UserRoleCd = isAccountPass.AccountInfo.UserRoleCd

					}, TimeSpan.FromMinutes(30));

					//레디스 세션 저장 상태 확인
					var retrievedModel = await _redisSessionStore.GetSessionAsync<AccountModel>($"userSession:{isAccountPass.AccountInfo.Idx}");
					
					Log.Info("SYSTEM", $"{isAccountPass.Result} - {isAccountPass.AccountInfo.ToString()}");

					return RedirectToAction(nameof(MainController.Main), "Main");
				}
				else
				{
					//아이디가 존재하지 않거나 비밀번호가 X
					Log.Info("SYSTEM", $"{isAccountPass.Result} - {isAccountPass.AccountInfo.ToString()}");
				}

				Log.Debug("SYSTEM", "=============================== LoginAction End ===============================");
			} catch (Exception ex) 
			{
				Log.Error("SYSTEM", $"An error occurred during login: {ex.Message}", ex);
			}

			return View("login_main"); // 로그인 페이지 다시 표시
		}
	}
}
