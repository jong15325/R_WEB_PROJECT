using R_WEB_PROJECT.Models.Log;
using R_WEB_PROJECT.Repositories.Log;
using R_WEB_PROJECT.Utilities.Log;
using static R_WEB_PROJECT.Utilities.Enums.LoginEnum;

namespace R_WEB_PROJECT.Services.Log
{
    public interface ILogLoginService
    {
        //아이디, 비밀번호로 계정 존재 여부 확인 서비스
        Task<int> InsertLogLoginAsync(LogLoginModel model);
    }

    public class LogLoginService : ILogLoginService
    {
        private readonly ILogLoginRepository _logLoginRepository;

        public LogLoginService(ILogLoginRepository logLoginRepository)
        {
            _logLoginRepository = logLoginRepository;
        }

        public async Task<int> InsertLogLoginAsync(LogLoginModel model)
        {
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== InsertLogLoginAsync Start ===============================");

                string status = GetResultMessage(LoginResult.Success);

                int result = await _logLoginRepository.InsertLogLoginAsync(model);

                LogUtil.Debug("SYSTEM", "=============================== InsertLogLoginAsync End ===============================");

                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during InsertLogLoginAsync Service : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
        }
    }
}
