using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Utilities.Manager;
using R_WEB_PROJECT.Utilities.Redis;

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
