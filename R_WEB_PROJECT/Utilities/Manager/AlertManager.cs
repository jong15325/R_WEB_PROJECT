using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Models.Alert;
using R_WEB_PROJECT.Utilities.Enums;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public class AlertManager
    {
        //TempData로 저장 한 Model을 컨트롤러에서 리다이렉트 후 AlertModel Json 형태로 전송 후 알럿 노출한다

        //베이직 알럿
        public static void BasicAlert(Controller controller, AlertType alertType, AlertIconType iconType, string title, string message)
        {
            AlertModel alert = new AlertModel { AlertType = alertType.GetDescription(), AlertIconType = iconType.GetDescription(), Title = title, Message = message};
            controller.TempData["AlertMessage"] = alert.ToJsonString();
        }

        //토스트 알럿
    }
}
