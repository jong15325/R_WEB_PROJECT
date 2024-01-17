using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models;
using R_WEB_PROJECT.Utilities.Enums;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public class AlertManager
    {
        public static void BasicAlert(Controller controller, string title, string message, AlertIconType iconType)
        {
            AlertModel alert = new AlertModel { AlertType = AlertType.Basic.GetDescription(), Title = title, Message = message, AlertIconType = iconType.GetDescription() };
            
            controller.TempData["AlertMessage"] = alert.ToJsonString();
        }

        public static void MixinAlert(Controller controller, string title, string message, AlertIconType iconType)
        {
            AlertModel alert =  new AlertModel { AlertType = AlertType.Mixin.GetDescription(), Title = title, Message = message, AlertIconType = iconType.GetDescription() };

            controller.TempData["AlertMessage"] = alert.ToJsonString();
        }
    }
}
