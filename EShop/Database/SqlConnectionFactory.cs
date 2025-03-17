using EShop.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EShop.Database
{
  /// <summary>
  /// Factory to create sql connections.
  /// </summary>
  public class SqlConnectionFactory : IDbConnectionFactory
  {
    private readonly string _connectionString;

    /// <summary>
    /// Initialize the <see cref="SqlConnectionFactory"/>.
    /// </summary>
    /// <param name="connectionString">Sql connection string.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SqlConnectionFactory(string connectionString)
    {
      _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    /// <inheritdoc/>
    public async Task<IDbConnection> CreateConnectionAsync()
    {
      var conn = new SqlConnection(_connectionString);
      await conn.OpenAsync();
      return conn;
    }
  }
}
