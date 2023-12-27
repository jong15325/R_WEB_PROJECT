using Microsoft.AspNetCore.Mvc;

namespace R_WEB_PROJECT.Controllers.Main
{
    public class MainController : Controller
    {
        //메인 페이지
        public IActionResult Main()
        {
            //변경확인
            return View("main");
        }
    }
}
