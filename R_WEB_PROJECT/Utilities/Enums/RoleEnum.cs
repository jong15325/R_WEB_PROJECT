using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
    public class RoleEnum
    {
        public enum UserRole
        {
            [Description("ADMIN")]
            ADMIN,

            [Description("USER")]
            USER,

            [Description("GUEST")]
            GUEST
        }
    }
}
