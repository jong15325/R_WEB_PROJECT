using R_WEB_PROJECT.Models;

namespace R_WEB_PROJECT.DTOs
{
    public class AccountValidDTO
    {
        public bool IsPass { get; set; }
        public AccountModel AccountInfo { get; set; }
        public string Result { get; set; }
    }
}
