using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Models;
using R_WEB_PROJECT.Services.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Log;
using R_WEB_PROJECT.Utilities.Manager;
using R_WEB_PROJECT.Utilities.Redis;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Controllers.Main
{
    public class MainController : Controller
    {
        private readonly RedisManager _redisSessionStore;
        private readonly MessageManager _messageManager;

        public MainController( RedisManager redisSessionStore, MessageManager messageManager)
        {
            _redisSessionStore = redisSessionStore;
            _messageManager = messageManager;
        }

        //메인 페이지
        public async Task<IActionResult> Main()
        {

            //변경확인
            return View("main");
        }
    }
}
