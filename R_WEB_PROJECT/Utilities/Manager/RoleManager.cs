using static R_WEB_PROJECT.Utilities.Enums.AuthEnum;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public static class RoleManager
    {
        public static Dictionary<UserRole, List<string>> RoleClaims = new Dictionary<UserRole, List<string>>
        {
            { UserRole.ADMIN, new List<string> { "ManageUsers", "ManageProducts", "ManageOrders" } },
            { UserRole.LEVEL0, new List<string> { "ViewProducts", "PlaceOrder" } }
        };
    }
}
