using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Models;
using R_WEB_PROJECT.RedisStore.Session;
using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.Utilities.Manager;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Controllers.Main
{
    public class MainController : Controller
    {
        private readonly RedisSessionStore _redisSessionStore;
        private readonly MessageManager _messageManager;

        public MainController( RedisSessionStore redisSessionStore, MessageManager messageManager)
        {
            _redisSessionStore = redisSessionStore;
            _messageManager = messageManager;
        }

        //메인 페이지
        public async Task<IActionResult> Main()
        {

            try
            {
                try
                {
                    var userSession = await _redisSessionStore.GetSessionAsync<AccountModel>("userSession:1");
                    if(userSession == null) {
                        // 세션 데이터를 뷰로 전달
                        ViewBag.UserSession = userSession;
                    }
                } catch (Exception ex)
                {
                    Log.Error("REDIS", $"An error occurred while Get the Redis session : {ex.Message}", ex);
                    AlertManager.BasicAlert(this, "", _messageManager.GetMessage("Session_NotFound"), AlertIconType.warning);
                }
            }
            catch (Exception ex) 
            {
                Log.Error("SYSTEM", $"An error occurred during Main: {ex.Message}", ex);
                AlertManager.BasicAlert(this, "", _messageManager.GetMessage("Login_Error"), AlertIconType.error);
            }
            

            
            //변경확인
            return View("main");
        }
    }
}
