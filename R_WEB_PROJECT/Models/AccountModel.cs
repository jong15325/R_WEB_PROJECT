using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R_WEB_PROJECT.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
            Idx = 0;
            UserId = string.Empty;
            UserType = string.Empty;
            UserPassword = string.Empty;
            UserPasswordSalt = string.Empty;
            UserName = string.Empty;
            UserRoleCd = string.Empty;
        }

        [Key]
        [Required]
        public int Idx { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string UserType { get; set; }

        [Required]
        [StringLength(255)]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string UserPasswordSalt { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string UserRoleCd { get; set; }

        [Required]
        public DateTime? UserCreateAt { get; set; }

        public DateTime? UserUpdateAt { get; set; }

        public DateTime? UserDeleteAt { get; set; }

        public override string ToString()
        {
            return $"Idx[{Idx}], UserId[{UserId}], UserName[{UserName}], UserRoleCd[{UserRoleCd}], UserCreateAt[{UserCreateAt}]," +
                $"UserUpdateAt[{UserUpdateAt}], UserDeleteAt[{UserDeleteAt}]";
        }
    }
}
