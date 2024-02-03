using R_WEB_PROJECT.Models.User;
using R_WEB_PROJECT.Utilities.Database;
using R_WEB_PROJECT.Utilities.Log;

namespace R_WEB_PROJECT.Repositories.Login
{
    public interface ILoginRepository
    {
        //아이디, 비밀번호로 계정 존재 여부 확인
        Task<AccountModel> selectAccountByIdAsync(AccountModel model);
    }
    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseManager _dbManager;

        public LoginRepository(DatabaseManager dbManager)
        {
            _dbManager = dbManager;
        }

        //아이디, 비밀번호로 계정 존재 여부 확인
        public async Task<AccountModel> selectAccountByIdAsync(AccountModel model)
        {
            const string query = "SELECT * FROM Account WHERE UserId = @UserId";

            object parameters = new { model.UserId };
            LogUtil.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

            try
            {
                LogUtil.Debug("SYSTEM", "=============================== selectAccountByIdAsync Repository Start ===============================");

                AccountModel result = await _dbManager.GetSingleRecordAsync<AccountModel>(query, parameters);

                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Error("SYSTEM", $"An error occurred during IsAccountByIdAsync Repository : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }
            finally
            {
                LogUtil.Debug("SYSTEM", "=============================== selectAccountByIdAsync Repository End ===============================");

            }
        }
    }
}
