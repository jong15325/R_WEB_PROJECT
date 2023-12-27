using log4net;
using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Models.Login;
using R_WEB_PROJECT.RedisStore.Session;
using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Log;

namespace R_WEB_PROJECT.Controllers.Login
{
    public class LoginController : Controller
    {
		//private readonly ILog _logger;
		private readonly ILoginService _loginService;
		private readonly RedisSessionStore _redisSessionStore;

		public LoginController(ILoginService loginService, RedisSessionStore redisSessionStore)
		{
			_loginService = loginService;
			_redisSessionStore = redisSessionStore;
			//_logger = LogManager.GetLogger(typeof(LoginController));
		}

		//로그인 메인 페이지
		[Route("/login/main")]
        public async Task<IActionResult> Login()
        {
			Log.Debug("SYSTEM", "Login");
			return View("login_main");
        }

		//로그인 프로세스
        [Route("/login/loginAction")]
        [HttpPost] // POST 메서드를 통해 폼 데이터를 처리
		public async Task<IActionResult> LoginAction(AccountModel model)
        {
			Log.Debug("SYSTEM", "LoginAction");

			bool isAuthenticated = await _loginService.IsAccountByIdPassAsync(model);
			Log.Debug("SYSTEM", $"isAuthenticated = {isAuthenticated}");

			if (isAuthenticated) {
				await _redisSessionStore.SetSessionAsync("userSession", model, TimeSpan.FromMinutes(30));
				Log.Debug("SYSTEM", $"isAuthenticated = {isAuthenticated}");
				Log.Debug("SYSTEM", $"Login Success {model.aId} / {model.aPassword}");
				return View("login_main");
			}

			return View("login_main"); // 로그인 페이지 다시 표시
		}
	}
}
