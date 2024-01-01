using R_WEB_PROJECT.Models.Login;
using R_WEB_PROJECT.Repositories.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Database;
using R_WEB_PROJECT.Utilities.Log;

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
		public async Task<AccountModel> IsAccountByIdPassAsync(AccountModel model)
		{
			var query = "SELECT A_NO, A_ID, A_PASSWORD, A_PASSWORD_SALT, A_NAME FROM TB_ACCOUNT WHERE A_ID = @aId";

			object parameters = new { aId = model.aId};
			Log.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

			AccountModel result = await _dbManager.GetSingleRecordAsync<AccountModel>(query, parameters);
			Log.Debug("SYSTEM", $"z aId = {result.aId}");
			return result;
		}
	}
}
