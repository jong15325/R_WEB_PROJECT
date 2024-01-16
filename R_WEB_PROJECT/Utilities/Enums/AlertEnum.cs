using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
    public class AlertEnum
    {
        public enum AlertType
        {
            [Description("Error")]
            Error,

            [Description("Scuccess")]
            Scuccess,

            [Description("Warning")]
            Warning,

            [Description("Info")]
            Info,
        }
    }
}
