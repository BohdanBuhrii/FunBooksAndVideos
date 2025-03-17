using EShop.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EShop.Database
{
  public class SqlConnectionFactory : IDbConnectionFactory
  {
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
      _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
      var conn = new SqlConnection(_connectionString);
      await conn.OpenAsync();
      return conn;
    }
  }
}
