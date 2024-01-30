using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
    public class AlertEnum
    {
        public enum AlertIconType
        {
            [Description("SUCCESS")]
            SUCCESS,

            [Description("ERROR")]
            ERROR,

            [Description("WARNING")]
            WARNING,

            [Description("INFO")]
            INFO,

            [Description("QUESTION")]
            QUESTION,
        }

        public enum AlertType
        {
            [Description("BASIC")]
            BASIC,

            [Description("CONFIRM")]
            CONFIRM,

            [Description("TIMER")]
            TIMER,

            [Description("MIXIN")]
            MIXIN,
        }
    }
}
