using static R_WEB_PROJECT.Utilities.Enums.AlertEnum;

namespace R_WEB_PROJECT.DTOs
{
    public class ResultDataDTO
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public AlertIconType IconType { get; set; }

        public bool Status { get; set; }

        public ResultDataDTO(string message, object data, AlertIconType iconType, bool status)
        {
            Message = message;
            Data = data;
            IconType = iconType;
            Status = status;
        }
    }
}
