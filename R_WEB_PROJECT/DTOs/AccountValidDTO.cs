using R_WEB_PROJECT.Models.User;

namespace R_WEB_PROJECT.DTOs
{
    public class AccountValidDTO
    {
        public AccountModel AccountInfo { get; set; }

        public bool IsPass { get; set; }

        public string Result { get; set; }
    }
}
