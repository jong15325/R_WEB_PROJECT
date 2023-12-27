using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace R_WEB_PROJECT.Utilities.Database
{
	public static class SqlPramMapper
	{
		public static string MapQuery(string query, SqlParameter[] parameters)
		{
			foreach (var parameter in parameters)
			{
				// 파라미터 이름 가져오기
				var paramName = parameter.ParameterName.TrimStart('@');

				// 정규식을 사용하여 쿼리와 일치하는 파라미터를 찾아 매핑
				var regex = new Regex($@"@{paramName}\b");
				var match = regex.Match(query);

				if (match.Success)
				{
					// 쿼리 문자열에서 일치하는 파라미터 부분을 실제 값으로 대체
					query = query.Replace(match.Value, "'"+parameter.Value.ToString()+"'");
				}
			}

			return query;
		}
	}
}
