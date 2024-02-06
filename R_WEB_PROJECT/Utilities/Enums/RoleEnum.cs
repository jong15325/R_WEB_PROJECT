using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
    public class RoleEnum
    {
        public enum UserRole
        {
            [Description("ROLE_AD")]
            ADMIN,

            [Description("ROLE_US")]
            USER,

            [Description("ROLE_GU")]
            GUEST,
            
            
        }
    }
}
