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
            const string query = "INSERT INTO LogLogin (LoginUserId, LoginAt, LoginIp, LoginAgent, LoginStatus) " +
                "VALUES (@LoginUserId, GETDATE(), @LoginIp, @LoginAgent, @LoginStatus)";

            object parameters = new
            {
                model.LoginUserId,
                model.LoginIp,
                model.LoginAgent,
                model.LoginStatus
            };

            LogUtil.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

            try
            {
                int result = await _dbManager.ExecuteNonQueryAsync(query, parameters);

                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during InsertLogLoginAsync Repository : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
        }
    }
}
