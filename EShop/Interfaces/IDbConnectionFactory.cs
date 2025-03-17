using System.Data;

namespace EShop.Interfaces
{
  /// <summary>
  /// Interface for generic database connection factory.
  /// </summary>
  public interface IDbConnectionFactory
  {
    /// <summary>
    /// Create new database connection.
    /// </summary>
    /// <returns>Abstract db connection.</returns>
    Task<IDbConnection> CreateConnectionAsync();
  }
}
