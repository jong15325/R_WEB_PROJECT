using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Models.User;
using System.Text.Json;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Utilities.Data
{
    public class TempDataUtil
    {
        public static void TempDataSet<T>(Controller controller, string key, T model) 
        {
            controller.TempData[key] = JsonSerializer.Serialize(model);
        }

        public static T TempDataGet<T>(Controller controller, string key)
        {
            var modelString = controller.TempData[key] as string;
            if (modelString != null)
            {
               return JsonSerializer.Deserialize<T>(modelString);
            }

            return default;
        }
    }
}
