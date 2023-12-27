using Microsoft.Data.SqlClient;
using System.Data;

public class DatabaseManager : IDisposable
{
	private readonly string _connectionString;
	private readonly SqlConnection _connection;

	public DatabaseManager(string connectionString)
	{
		_connectionString = connectionString;
		_connection = new SqlConnection(_connectionString);
	}

	// 커넥션 OPEN
	private async Task OpenConnectionAsync()
	{
		if (_connection.State != ConnectionState.Open)
			await _connection.OpenAsync();
	}

	//커넥션 GET
	public async Task<SqlConnection> GetConnectionAsync()
	{
		await OpenConnectionAsync();
		return _connection;
	}

	//단일 레코드 조회
	public async Task<object> GetSingleRecordAsync(string query, SqlParameter[] parameters = null)
	{
		using (var connection = await GetConnectionAsync()) {
			using (var command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				return await command.ExecuteScalarAsync();
			}
		}
	}

	//다중 레코드 조회
	public async Task<DataTable> GetMultiRecordAsync(string query, SqlParameter[] parameters = null)
	{
		using (var connection = await GetConnectionAsync()) {
			using (var command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				var dataTable = new DataTable();
				var reader = await command.ExecuteReaderAsync();

				dataTable.Load(reader);
				return dataTable;
			}
		}
	}

	//쿼리 실행 및 성공 행 수 반환
	public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null)
	{
		using (var connection = await GetConnectionAsync()) {
			using (var command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				return await command.ExecuteNonQueryAsync();
			}
		}
	}

	//한꺼번에 트랙잭션 처리
	public async Task ExecuteTransactionAsync(string[] queries, SqlParameter[][] parameters = null, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
	{
		using (var connection = await GetConnectionAsync())
		{
			var transaction = connection.BeginTransaction(isolationLevel);

			try
			{
				foreach (var query in queries)
				{
					using (var command = new SqlCommand(query, connection, transaction))
					{
						if (parameters != null && parameters.Length > 0)
						{
							command.Parameters.AddRange(parameters[Array.IndexOf(queries, query)]);
						}

						await command.ExecuteNonQueryAsync();
					}
				}

				transaction.Commit();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw;
			}
		}
	}

	//벌크 인서트
	public async Task BulkInsertAsync(DataTable dataTable, string tableName)
	{
		using (var connection = new SqlConnection(_connectionString))
		{
			await connection.OpenAsync();

			using (var bulkCopy = new SqlBulkCopy(connection))
			{
				bulkCopy.DestinationTableName = tableName;

				foreach (DataColumn column in dataTable.Columns)
				{
					bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
				}

				await bulkCopy.WriteToServerAsync(dataTable);
			}
		}
	}

	//인서트 및 IDX 반환
	public async Task<int> InsertAndReturnIdAsync(string query, SqlParameter[] parameters = null)
	{
		using (var connection = await GetConnectionAsync())
		{
			query += ";SELECT SCOPE_IDENTITY();"; // 이 부분은 해당 테이블의 자동 증가하는 ID 값을 반환하는 SQL 문을 사용하는 예시입니다.

			using (var command = new SqlCommand(query, connection))
			{
				if (parameters != null)
					command.Parameters.AddRange(parameters);

				var result = await command.ExecuteScalarAsync();
				return Convert.ToInt32(result);
			}
		}
	}

	public void Dispose()
	{
		_connection?.Dispose();
	}
}