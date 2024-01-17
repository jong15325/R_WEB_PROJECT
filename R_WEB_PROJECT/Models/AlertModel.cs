using System.Text.Json;

namespace R_WEB_PROJECT.Models
{
    public class AlertModel
    {
        public AlertModel()
        {
            Title = string.Empty;
            Message = string.Empty;
            AlertType = string.Empty;
            AlertIconType = string.Empty;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public string AlertType { get; set; }

        public string AlertIconType { get; set; }

        //TempData는 String반환이라 json으로 변환해줘야함
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static AlertModel FromJsonString(string jsonString)
        {
            return JsonSerializer.Deserialize<AlertModel>(jsonString);
        }
    }
}
