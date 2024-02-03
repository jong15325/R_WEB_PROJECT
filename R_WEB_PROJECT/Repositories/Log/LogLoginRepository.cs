using R_WEB_PROJECT.Models.Log;
using R_WEB_PROJECT.Utilities.Database;
using R_WEB_PROJECT.Utilities.Log;

namespace R_WEB_PROJECT.Repositories.Log
{
    public interface ILogLoginRepository
    {
        //아이디, 비밀번호로 계정 존재 여부 확인
        Task<int> InsertLogLoginAsync(LogLoginModel model);
    }

    public class LogLoginRepository : ILogLoginRepository
    {
        private readonly DatabaseManager _dbManager;

        public LogLoginRepository(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        //아이디, 비밀번호로 계정 존재 여부 확인
        public async Task<int> InsertLogLoginAsync(LogLoginModel model)
        {
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== InsertLogLoginAsync Repository Start ===============================");

                const string query = "INSERT INTO LogLogin (LoginUserId, LoginAt, LoginIp, LoginAgent, LoginStatusCode, LoginMessage) " +
               "VALUES (@LoginUserId, GETDATE(), @LoginIp, @LoginAgent, @LoginStatusCode, @LoginMessage)";

                object parameters = new
                {
                    model.LoginUserId,
                    model.LoginIp,
                    model.LoginAgent,
                    model.LoginStatusCode,
                    model.LoginMessage
                };

                LogUtil.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

                int result = await _dbManager.ExecuteNonQueryAsync(query, parameters);

                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during InsertLogLoginAsync Repository : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
            finally
            {
                LogUtil.Debug("SYSTEM", "=============================== InsertLogLoginAsync Repository End ===============================");

            }
        }
    }
}
