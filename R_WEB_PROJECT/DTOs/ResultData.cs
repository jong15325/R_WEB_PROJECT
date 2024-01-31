using R_WEB_PROJECT.Utilities.Enums;

namespace R_WEB_PROJECT.DTOs
{
    public class ResultData
    {
        public AlertEnum.AlertType AlertType { get; set; }

        public AlertEnum.AlertIconType  AlertIconType { get; set; } 

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public ResultData(AlertEnum.AlertType alertType, AlertEnum.AlertIconType alertIconType, string message, int statusCode)
        {
            AlertType = alertType;
            AlertIconType = alertIconType;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
