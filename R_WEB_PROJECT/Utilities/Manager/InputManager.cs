using Microsoft.AspNetCore.Mvc;
using R_WEB_PROJECT.Models;
using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public class InputManager
    {
        public static void SetModelInViewBag<T>(Controller controller, T model)
        {
            // 모델의 속성을 순회하면서 ViewBag에 설정
            foreach (var property in typeof(T).GetProperties())
            {
                object value = property.GetValue(model);

                // ViewBag에 속성 값 설정 (형식 변환)
                controller.ViewBag[property.Name] = value != null ? value.ToString() : null;
            }
        }
    }
}
