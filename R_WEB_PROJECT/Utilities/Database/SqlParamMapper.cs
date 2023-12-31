using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace R_WEB_PROJECT.Utilities.Database
{
	public static class SqlParamMapper
	{
		public static string MapQuery(string query, object parameters)
		{
			if (parameters != null)
			{
				var properties = parameters.GetType().GetProperties();

				foreach (var prop in properties)
				{
					// 파라미터 이름 가져오기
					var paramName = prop.Name;
					var paramValue = prop.GetValue(parameters);

					// 정규식을 사용하여 쿼리와 일치하는 파라미터를 찾아 매핑
					var regex = new Regex($@"@{paramName}\b");
					var match = regex.Match(query);

					if (match.Success)
					{
						query = query.Replace(match.Value, "'" + paramValue?.ToString() + "'");
					}
				}
			}
				

			return query;
		}
	}
}
