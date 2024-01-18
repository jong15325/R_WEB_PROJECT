using R_WEB_PROJECT.Models.User;
using R_WEB_PROJECT.Repositories.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Database;
using R_WEB_PROJECT.Utilities.Log;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace R_WEB_PROJECT.Repositories.Implementation.Login
{
    public class LoginRepository : ILoginRepository
	{
		private readonly DatabaseManager _dbManager;

		public LoginRepository(DatabaseManager dbManager)
		{
			_dbManager = dbManager;
		}

		//아이디, 비밀번호로 계정 존재 여부 확인
		public async Task<AccountModel> IsAccountByIdAsync(AccountModel model)
		{
			const string query = "SELECT * FROM Account WHERE UserId = @UserId";

			object parameters = new { UserId = model.UserId };
			Log.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

			try
            {
                AccountModel result = await _dbManager.GetSingleRecordAsync<AccountModel>(query, parameters);

                return result;
            }
            catch (Exception ex) 
			{
                Log.Error("SYSTEM", $"An error occurred during IsAccountByIdAsync Repository : {ex.GetType().Name} - {ex.Message}", ex);
                throw;
            }


			
		}
	}
}
