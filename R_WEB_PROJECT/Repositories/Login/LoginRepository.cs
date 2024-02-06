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
            try
            {
                LogUtil.Debug("SYSTEM", "=============================== selectAccountByIdAsync Repository Start ===============================");

                const string query = "SELECT A.*, R.RoleCd as UserRoleCd, R.RoleName as UserRoleName FROM Account A" +
                    " LEFT JOIN UserRole UR ON A.idx = UR.UserIdx" +
                    " LEFT JOIN ROLE R ON R.idx = UR.RoleIdx" +
                    " WHERE UserId = @UserId";

                object parameters = new { model.UserId };
                LogUtil.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

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
