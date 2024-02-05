using JWTAuthAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Controllers.Main;
using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models.Log;
using R_WEB_PROJECT.Models.User;
using R_WEB_PROJECT.Services.Log;
using R_WEB_PROJECT.Services.Login;
using R_WEB_PROJECT.Utilities.Common.Function;
using R_WEB_PROJECT.Utilities.Data;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.Utilities.Manager;
using R_WEB_PROJECT.Utilities.Mapper;
using R_WEB_PROJECT.Utilities.Redis;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;
using static R_WEB_PROJECT.Utilities.Enums.StatusEnum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace R_WEB_PROJECT.Controllers.Login
{
    public class LoginController : Controller
    { 
		private readonly ILoginService _loginService;
        private readonly ILogLoginService _logLoginService;
        private readonly IJWTAuthService _jwtAuthService;
        private readonly RedisManager _redisSessionStore;
        private readonly ResourceManager _messageManager;
        private readonly UserInfoManager _userInfoManager;

        public LoginController(ILoginService loginService, ILogLoginService logLoginService, IJWTAuthService jwtAuthService,
            RedisManager redisSessionStore, ResourceManager messageManager, UserInfoManager userInfoManager)
		{
			/*서비스*/
            _loginService = loginService;
            _logLoginService = logLoginService;
            _jwtAuthService = jwtAuthService;

            _redisSessionStore = redisSessionStore;

            /*매니저*/
            _messageManager = messageManager;
            _userInfoManager = userInfoManager;
        }

        //로그인 메인 페이지
        [Route("/login/main")]
        public IActionResult Login()
        {
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== LoginPage Start ===============================");
                var model = TempDataUtil.TempDataGet<AccountModel>(this, "formResult") ?? new AccountModel();

                return View("login_main", model);
            }
            catch (Exception ex)
			{
				LogUtil.Error("SYSTEM", $"An error occurred during login: {ex.Message}", ex);
                return RedirectToAction(nameof(Error), "Error");
            }
            finally
            {
                LogUtil.Debug("SYSTEM", "=============================== LoginPage End ===============================");
            }
        }

		//로그인 프로세스
        [Route("/login/loginAction")]
        [HttpPost] // POST 메서드를 통해 폼 데이터를 처리
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> LoginAction(AccountModel model)
        {
            ResultData resultData = new ResultData(AlertType.BASIC, AlertIconType.SUCCESS, "", "", 0);

            try
            {
                LogUtil.Debug("SYSTEM", "=============================== LoginAction Start ===============================");

                if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.UserPassword))
                {
                    // UserId 또는 UserPassword가 비어있는 경우 처리
                    LogUtil.Warn("SYSTEM", "아이디 또는 비밀번호를 입력하지 않고 로그인을 시도했습니다.");
                    resultData = new ResultData(AlertType.BASIC, AlertIconType.WARNING, "", _messageManager.GetMessage("Login_EnterIdPasswd"), (int)LoginStatusCode.LOGIN_ENTERIDPASSWD);

                    return RedirectToAction(nameof(Login), "Login");
                }

                //로그인 검증
                AccountValidDTO account = await _loginService.selectAccountByIdAsync(model);
                LogUtil.Info("SYSTEM", $"로그인 시도 UserId = {model.UserId} / 아이디 검증 결과 = {account.IsPass}");

                if (account.IsPass)
                {
                    if (!CoreFunction.IsUserLockCheck(account))
                    {
                        //JWTAuthAPI 호출 토큰 생성 및 반환
                        // 로그인 검증
                        var token = await _jwtAuthService.AuthenticateUserAsync(account);

                        if (string.IsNullOrEmpty(token))
                        {
                            LogUtil.Info("SYSTEM", $"{account.Result} - {model.ToString()}");
                            resultData = new ResultData(AlertType.BASIC, AlertIconType.ERROR, "", _messageManager.GetMessage("Login_Token_Failed"), (int)LoginStatusCode.LOGIN_TOKEN_FAILED);
                            
                            return RedirectToAction(nameof(Login), "Login");
                        }

                        try
                        {
                            //레디스 세션 저장
                            await _redisSessionStore.SetRedisAsync($"userSession:{account.AccountInfo.Idx}", new RedisSessionData
                            {
                                Idx = account.AccountInfo.Idx,
                                UserId = account.AccountInfo.UserId,
                                UserType = account.AccountInfo.UserType,
                                UserName = account.AccountInfo.UserName,
                                UserRoleCd = account.AccountInfo.UserRoleCd,
                                UserToken = token

                            }, TimeSpan.FromMinutes(30));
                        }
                        catch (Exception ex)
                        {
                            LogUtil.Error("REDIS", $"An error occurred while saving the Redis session : {ex.GetType().Name} - {ex.Message}", ex);
                            resultData = new ResultData(AlertType.BASIC, AlertIconType.ERROR, "", _messageManager.GetMessage("Login_Error"), (int)LoginStatusCode.LOGIN_ERROR);

                            return RedirectToAction(nameof(Login), "Login");
                        }

                        //로그인 성공
                        LogUtil.Info("SYSTEM", $"{account.Result} - {account.AccountInfo.ToString()}");
                        resultData = new ResultData(AlertType.MIXIN, AlertIconType.SUCCESS, _messageManager.GetMessage("Login_Success"), "", (int)LoginStatusCode.LOGIN_SUCCESS);

                        return RedirectToAction(nameof(MainController.Main), "Main");
                    }
                    else
                    {
                        //계정 잠금 상태
                        LogUtil.Info("SYSTEM", $"{account.Result} - {model.ToString()}");
                        resultData = new ResultData(AlertType.BASIC, AlertIconType.WARNING, "", _messageManager.GetMessage("Login_Lock"), (int)LoginStatusCode.LOGIN_LOCK);
                    }
                }
                else 
                {
                    //아이디가 존재하지 않거나 비밀번호가 존재하지 않을 경우
                    LogUtil.Info("SYSTEM", $"{account.Result} - {model.ToString()}");
                    resultData = new ResultData(AlertType.BASIC, AlertIconType.WARNING, "", _messageManager.GetMessage("Login_Invalid"), (int)LoginStatusCode.LOGIN_INVALID);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during login : {ex.GetType().Name} - {ex.Message}", ex);
                resultData = new ResultData(AlertType.BASIC, AlertIconType.ERROR, "", _messageManager.GetMessage("Login_Error"), (int)LoginStatusCode.LOGIN_ERROR);

                return RedirectToAction(nameof(Login), "Login");
            }
            finally 
            {
                //로그인 이력 저장
                await _logLoginService.InsertLogLoginAsync(new LogLoginModel
                {
                    LoginUserId = model.UserId,
                    LoginIp = _userInfoManager.GetUserIPAddress(),
                    LoginAgent = _userInfoManager.GetUserAgent(),
                    LoginStatusCode = resultData.StatusCode,
                    LoginMessage = _messageManager.GetMessage(resultData.Message)
                });

                //Model을 DTO에 매핑 -> tempData 저장
                AccountDTO resultDTO = MappingProfile.ResultAccount(model);
                TempDataUtil.TempDataSet(this, "formResult", resultDTO);

                AlertManager.BasicAlert(this, resultData.AlertType, resultData.AlertIconType, _messageManager.GetMessage(resultData.Title), _messageManager.GetMessage(resultData.Message));

                LogUtil.Debug("SYSTEM", "=============================== LoginAction End ===============================");
            }

            return RedirectToAction(nameof(Login), "Login");
        }

        //로그인 메인 페이지
        [Route("/login/register")]
        [Authorize(Roles = UserRolePolicies.UserPolicy)]
        public IActionResult Register()
        {
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== RegisterPage Start ===============================");
                var model = TempDataUtil.TempDataGet<AccountModel>(this, "formResult") ?? new AccountModel();

                return View("login_register", model);
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during login: {ex.Message}", ex);
                return RedirectToAction(nameof(Error), "Error");
            }
            finally
            {
                LogUtil.Debug("SYSTEM", "=============================== RegisterPage End ===============================");
            }
        }

    }
}
