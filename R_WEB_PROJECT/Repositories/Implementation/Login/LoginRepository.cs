using Dapper;
using log4net.Repository.Hierarchy;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using R_WEB_PROJECT.Models.Login;
using R_WEB_PROJECT.Repositories.Abstraction.Login;
using R_WEB_PROJECT.Utilities.Database;
using R_WEB_PROJECT.Utilities.Log;
using System.Data;
using System.Diagnostics;
using System.Reflection;

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
			var query = "SELECT A_NO, A_ID, A_PASSWORD, A_PASSWORD_SALT, A_NAME FROM TB_ACCOUNT WHERE A_ID = @aId AND A_PASSWORD = @aPassword";

			object parameters = new { aId = model.aId, aPassword = model.aPassword };
			Log.Debug("SQL", SqlParamMapper.MapQuery(query, parameters));

			var result = await _dbManager.GetSingleRecordAsync<AccountModel>(query, parameters);

			return result;
		}
	}
}
