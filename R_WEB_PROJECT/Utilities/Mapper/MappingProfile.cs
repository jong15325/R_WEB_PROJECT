using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models.User;

namespace R_WEB_PROJECT.Utilities.Mapper
{
    public class MappingProfile
    {
        //model과 DTO를 매핑 하여 민감한 정보를 제외하고 DTO 반환

        public static AccountDTO ResultAccount(AccountModel model)
        {
            return new AccountDTO
            {
                UserId = model.UserId,
                UserType = model.UserType,
                UserName = model.UserName,
                UserRoleCd = model.UserRoleCd
            };
        }
    }
}
