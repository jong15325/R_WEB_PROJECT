using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

public class DatabaseManager : IDisposable
{
	private readonly string _connectionString;

	public DatabaseManager(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<IDbConnection> GetOpenConnectionAsync()
	{
		var connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		return connection;
	}

	//단일 레코드 조회
	public async Task<T> GetSingleRecordAsync<T>(string query, object parameters)
	{
		using (var connection = await GetOpenConnectionAsync())
		{
			return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
		}
	}

	//다중 레코드 조회
	public async Task<IEnumerable<T>> GetMultiRecordsAsync<T>(string query, object parameters)
	{
		using (var connection = await GetOpenConnectionAsync())
		{
			return await connection.QueryAsync<T>(query, parameters);
		}
	}

	//NSERT, UPDATE, DELETE 실행 후 행 갯수 반환
	public async Task<int> ExecuteNonQueryAsync(string query, object parameters)
	{
		using (var connection = await GetOpenConnectionAsync())
		{
			return await connection.ExecuteAsync(query, parameters);
		}
	}

	public async Task<int> InsertAndReturnIdAsync(string query, object parameters)
	{
		using (var connection = await GetOpenConnectionAsync())
		{
			query += ";SELECT SCOPE_IDENTITY();";
			return await connection.ExecuteScalarAsync<int>(query, parameters);
		}
	}

	public void Dispose()
	{
	}
}