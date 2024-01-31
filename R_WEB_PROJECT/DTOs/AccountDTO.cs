using System.ComponentModel.DataAnnotations;

namespace R_WEB_PROJECT.DTOs
{
    public class AccountDTO
    {
        public string UserId { get; set; }

        public string UserType { get; set; }

        public string UserName { get; set; }

        public string UserRoleCd { get; set; }
    }
}
