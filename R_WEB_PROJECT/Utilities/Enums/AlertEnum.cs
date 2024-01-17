using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
    public class AlertEnum
    {
        public enum AlertIconType
        {
            [Description("success")]
            success,

            [Description("error")]
            error,

            [Description("warning")]
            warning,

            [Description("info")]
            info,

            [Description("question")]
            question,
        }

        public enum AlertType
        {
            [Description("Basic")]
            Basic,

            [Description("Confirm")]
            Confirm,

            [Description("Timer")]
            Timer,

            [Description("Mixin")]
            Mixin,
        }
    }
}
