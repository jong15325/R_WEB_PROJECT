using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.Utilities.Data
{
    public class ResultDataUtil
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public AlertIconType IconType { get; set; }

        public ResultDataUtil(string message, object data, AlertIconType iconType)
        {
            Message = message;
            Data = data;
            IconType = iconType;
        }
    }
}
