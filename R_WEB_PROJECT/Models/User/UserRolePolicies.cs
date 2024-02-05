using Microsoft.AspNetCore.Authorization;
using R_WEB_PROJECT.Utilities.Enums;
using static R_WEB_PROJECT.Utilities.Enums.RoleEnum;

namespace JWTAuthAPI.Models.User
{
    public class UserRolePolicies
    {
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(UserRole.ADMIN.GetDescription()).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(UserRole.USER.GetDescription()).Build();
        }

        public static AuthorizationPolicy GuestPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(UserRole.GUEST.GetDescription()).Build();
        }
    }
}
