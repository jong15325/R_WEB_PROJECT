using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models.Log;
using R_WEB_PROJECT.Models.User;

namespace R_WEB_PROJECT.Services.Abstraction.Log
{
    public class ILogLoginService
    {
        //아이디, 비밀번호로 계정 존재 여부 확인 서비스
        Task<LogLoginModel> InsertLogLoginAsync(LogLoginModel model);
    }
}
