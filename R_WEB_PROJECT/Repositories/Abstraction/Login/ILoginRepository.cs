using R_WEB_PROJECT.Models.User;
using System.Data;

namespace R_WEB_PROJECT.Repositories.Abstraction.Login
{
    public interface ILoginRepository
    {
        //아이디, 비밀번호로 계정 존재 여부 확인
        Task<AccountModel> IsAccountByIdAsync(AccountModel model);
    }
}
